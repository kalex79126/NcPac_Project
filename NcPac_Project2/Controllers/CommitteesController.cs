using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NcPac_Project2.Utilities;
using NuGet.Configuration;
using Committee = NcPac_Project2.Models.Committee;

namespace NcPac_Project2.Controllers
{
    [Authorize]
    public class CommitteesController : Controller
    {
        private readonly NC_PAC_Context _context;

        public CommitteesController(NC_PAC_Context context)
        {
            _context = context;
        }

        // GET: Committees
        public async Task<IActionResult> Index(int? page, int? pageSizeID, int? Division,int? NcStaffID, string SearchName, string actionButton, string sortFieldID,  string sortDirectionCheck, string sortDirection = "asc", string sortField = "Committee Name")
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);
            ViewData["FilteringData"] = "filterbuttons";
            DropDownList();

            string[] sortOptions = new[] { "Committee Name" };

            var committees = _context.Committees
                .Include(c => c.Division)
                .Include(c => c.MemberCommitteeEnrolls)
                .AsNoTracking();

            if (Division.HasValue)
            {
                committees = committees.Where(c => c.Division.DivisionID == Division);
                ViewData["FilteringData"] = "btn-danger";
            }
            //if (NcStaffID.HasValue)
            //{
            //    committees = committees.Where(c => c.NcStaff.NcStaffID == NcStaffID);
            //    ViewData["FilteringData"] = "btn-danger";
            //}
            if (!String.IsNullOrEmpty(SearchName))
            {
                committees = committees.Where(p => p.CommitteeName.ToUpper().Contains(SearchName.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(actionButton))
            {
                page = 1;
                if (sortOptions.Contains(actionButton))
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
                else
                {
                    sortDirection = String.IsNullOrEmpty(sortDirectionCheck) ? "asc" : "desc";
                    sortField = sortFieldID;
                }
            }
            if (sortField == "Committee Name")
            {
                if (sortDirection == "asc")
                {
                    committees = committees
                        .OrderBy(p => p.CommitteeName);
                }
                else
                {
                    committees = committees
                        .OrderByDescending(p => p.CommitteeName);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    committees = committees
                        .OrderBy(p => p.CommitteeName);
                }
                else
                {
                    committees = committees
                        .OrderByDescending(p => p.CommitteeName);
                }
            }
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Committees");
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Committee>.CreateAsync(committees.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
        }

        // GET: Committees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Committees == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .Include(c => c.Division)
                .Include(c => c.MemberCommitteeEnrolls)
                .FirstOrDefaultAsync(m => m.CommitteeID == id);
            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // GET: Committees/Create
        [Authorize(Roles = "Top Admin")]
        public IActionResult Create()
        {
            ViewDataReturnURL();
            ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionTitle");
            //ViewData["NcStaffID"] = new SelectList(_context.NcStaffs, "NcStaffID", "StaffFullName");
            return View();
        }

        // POST: Committees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Create([Bind("CommitteeID,CommitteeName,DivisionID,NcStaffID,VolunteerID")] Committee committee)
        //("CommitteeID,CommitteeName,DivisionID")] Committee committee)
        {
            ViewDataReturnURL();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(committee);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "You have successfully created a Committee.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Can't save changes even after multiple attempts. Please try again.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("DivisionID", "Can't save changes");
                }
                else
                {
                    ModelState.AddModelError("", "Can't save changes. Please try again");
                }
            }

            ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionTitle", committee.DivisionID);
            //ViewData["NcStaffID"] = new SelectList(_context.NcStaffs, "NcStaffID", "StaffFullName", committee.NcStaffID);
            return View(committee);
        }

        // GET: Committees/Edit/5
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Committees == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees.FindAsync(id);
            if (committee == null)
            {
                return NotFound();
            }
            ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionTitle", committee.DivisionID);
            //ViewData["NcStaffID"] = new SelectList(_context.NcStaffs, "NcStaffID", "StaffFullName", committee.NcStaffID);
            return View(committee);
        }

        // POST: Committees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        //public async Task<IActionResult> Edit(int id, Byte[] RowVersion)
        public async Task<IActionResult> Edit(int id, [Bind("CommitteeID,CommitteeName,DivisionID,NcStaffID")] Committee committee)
        {
            ViewDataReturnURL();
            var committeeToUpdate = await _context.Committees
                .FirstOrDefaultAsync(m => m.CommitteeID == id);

            if (committeeToUpdate == null)
            {
                return NotFound();
            }

            _context.Entry(committeeToUpdate);

            if (await TryUpdateModelAsync<Committee>(committeeToUpdate, "", m => m.CommitteeName, m => m.DivisionID ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "You have successfully updated a Committee.";
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Can't save changes even after multiple attempts. Please try again.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommitteeExists(committeeToUpdate.CommitteeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                           + "was modified by another user. Please go back and refresh.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            DropDownList(committeeToUpdate);
            //ViewData["DivisionID"] = new SelectList(_context.Divisions, "DivisionID", "DivisionTitle", committeeToUpdate.DivisionID);
            //ViewData["NcStaffID"] = new SelectList(_context.NcStaffs, "NcStaffID", "NcStaffFirstName", committee.NcStaffID);
            return View(committeeToUpdate);
        }

        // GET: Committees/Delete/5
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Committees == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .Include(c => c.Division)
                .Include(c => c.MemberCommitteeEnrolls)
                .FirstOrDefaultAsync(m => m.CommitteeID == id);
            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // POST: Committees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewDataReturnURL();
            if (_context.Committees == null)
            {
                return Problem("Entity set 'NC_PAC_Context.Committees'  is null.");
            }
            var committee = await _context.Committees.FindAsync(id);
            if (committee != null)
            {
                _context.Committees.Remove(committee);
            }
            
            await _context.SaveChangesAsync();
            TempData["Message"] = "You have successfully deleted a Committee.";
            return RedirectToAction(nameof(Index));
        }

        public void DropDownList(Committee committee = null)
        {
            ViewData["Division"] = new SelectList(_context.Divisions.OrderBy(i => i.DivisionTitle), "DivisionID", "DivisionTitle");
            //ViewData["NcStaffID"] = new SelectList(_context.NcStaffs.OrderBy(i => i.NcStaffLastName), "NcStaffID", "StaffFullName");
            //ViewData["MemberID"] = new SelectList(_context.Members.OrderBy(i => i.MemberLastName), "MemberID", "MemFullName");
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }

        private bool CommitteeExists(int id)
        {
          return _context.Committees.Any(e => e.CommitteeID == id);
        }
    }
}
