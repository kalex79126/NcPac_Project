using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NcPac_Project2.Models.NotInDataModel;
using NcPac_Project2.Utilities;
using NuGet.Protocol.Plugins;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NcPac_Project2.Controllers
{
    [Authorize]
    public class ActionsController : Controller
    {
        private readonly NC_PAC_Context _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ActionsController(NC_PAC_Context context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Actions
        public async Task<IActionResult> Index(int? page, int? pageSizeID, int? Meeting, bool? IsCompleted, int? MemberID,  string SearchName, string SearchDescription, string actionButton, string sortFieldID, string SearchStartDueDate, string SearchEndDueDate, string sortDirectionCheck, string sortDirection = "asc", string sortField = "Name")
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);
            ViewData["FilteringData"] = "filterbuttons";
            DropDownList();

            string[] sortOptions = new[] { "Name", "Description", "DueDate" };
            if ((_signInManager.IsSignedIn(User) && User.IsInRole("User")))
            {
                var actionitems = _context.ActionItems
                    .Include(t => t.Meeting)
                    .Include(t => t.Member)
                    .Where(e => e.Member.MemberEmail == User.Identity.Name)
                    .AsNoTracking();

                if (Meeting.HasValue)
                {
                    actionitems = actionitems.Where(i => i.MeetingID == Meeting);
                    ViewData["FilteringData"] = "btn-danger";
                }
                
                if (MemberID.HasValue)
                {
                    actionitems = actionitems.Where(i => i.MemberID == MemberID);
                    ViewData["FilteringData"] = "btn-danger";
                }
                if (IsCompleted.HasValue)
                {
                    if (IsCompleted.Value == true)
                    {
                        actionitems = actionitems.Where(i => i.IsCompleted == true);
                        ViewData["FilteringData"] = "btn-danger";
                    }
                    else if (IsCompleted.Value == false)
                    {
                        actionitems = actionitems.Where(i => i.IsCompleted == false);
                        ViewData["FilteringData"] = "btn-danger";
                    }
                }
                if (!String.IsNullOrEmpty(SearchName))
                {
                    actionitems = actionitems.Where(p => p.ActionName.ToUpper().Contains(SearchName.ToUpper()));
                    ViewData["FilteringData"] = "btn-danger";
                }
                if (!String.IsNullOrEmpty(SearchDescription))
                {
                    actionitems = actionitems.Where(p => p.ActionDescription.ToUpper().Contains(SearchDescription.ToUpper()));
                    ViewData["FilteringData"] = "btn-danger";
                }
                if (!String.IsNullOrEmpty(SearchStartDueDate) && !String.IsNullOrEmpty(SearchEndDueDate))
                {
                    if (DateTime.Parse(SearchEndDueDate) < DateTime.Parse(SearchStartDueDate))
                    {
                        ModelState.AddModelError(string.Empty, "Start date can't be before End date");
                    }
                    else
                    {
                        actionitems = actionitems.Where(p => p.ActionDueDate >= DateTime.Parse(SearchStartDueDate) && p.ActionDueDate <= DateTime.Parse(SearchEndDueDate));
                        ViewData["FilteringData"] = "btn-danger";
                    }
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
                if (sortField == "Name")
                {
                    if (sortDirection == "asc")
                    {
                        actionitems = actionitems
                            .OrderBy(p => p.ActionName);
                    }
                    else
                    {
                        actionitems = actionitems
                            .OrderByDescending(p => p.ActionName);
                    }
                }
                else if (sortField == "Description")
                {
                    if (sortDirection == "asc")
                    {
                        actionitems = actionitems
                            .OrderBy(p => p.ActionDescription);
                    }
                    else
                    {
                        actionitems = actionitems
                            .OrderByDescending(p => p.ActionDescription);
                    }
                }
                else
                {
                    if (sortDirection == "asc")
                    {
                        actionitems = actionitems
                            .OrderBy(p => p.ActionDueDate);
                    }
                    else
                    {
                        actionitems = actionitems
                            .OrderByDescending(p => p.ActionDueDate);
                    }
                }
                ViewData["sortField"] = sortField;
                ViewData["sortDirection"] = sortDirection;
                ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

                int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Actions");
                ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
                var pagedData = await PaginatedList<ActionItem>.CreateAsync(actionitems.AsNoTracking(), page ?? 1, pageSize);
                return View(pagedData);
            }
            else
            {
                var actionitems = _context.ActionItems.Include(t => t.Meeting).Include(t => t.Member).AsNoTracking();

                if (Meeting.HasValue)
                {
                    actionitems = actionitems.Where(i => i.MeetingID == Meeting);
                    ViewData["FilteringData"] = "btn-danger";
                }
                if (MemberID.HasValue)
                {
                    actionitems = actionitems.Where(i => i.MemberID == MemberID);
                    ViewData["FilteringData"] = "btn-danger";
                }
                if (IsCompleted.HasValue)
                {
                    if(IsCompleted.Value == true)
                    {
                        actionitems = actionitems.Where(i => i.IsCompleted == true);
                        ViewData["FilteringData"] = "btn-danger";
                    }
                    else if (IsCompleted.Value == false)
                    {
                        actionitems = actionitems.Where(i => i.IsCompleted == false);
                        ViewData["FilteringData"] = "btn-danger";
                    }
                }
                if (!String.IsNullOrEmpty(SearchName))
                {
                    actionitems = actionitems.Where(p => p.ActionName.ToUpper().Contains(SearchName.ToUpper()));
                    ViewData["FilteringData"] = "btn-danger";
                }
                if (!String.IsNullOrEmpty(SearchDescription))
                {
                    actionitems = actionitems.Where(p => p.ActionDescription.ToUpper().Contains(SearchDescription.ToUpper()));
                    ViewData["FilteringData"] = "btn-danger";
                }
                if (!String.IsNullOrEmpty(SearchStartDueDate) && !String.IsNullOrEmpty(SearchEndDueDate))
                {
                    if (DateTime.Parse(SearchEndDueDate) < DateTime.Parse(SearchStartDueDate))
                    {
                        ModelState.AddModelError(string.Empty, "Start date can't be before End date");
                    }
                    else
                    {
                        actionitems = actionitems.Where(p => p.ActionDueDate >= DateTime.Parse(SearchStartDueDate) && p.ActionDueDate <= DateTime.Parse(SearchEndDueDate));
                        ViewData["FilteringData"] = "btn-danger";
                    }
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
                if (sortField == "Name")
                {
                    if (sortDirection == "asc")
                    {
                        actionitems = actionitems
                            .OrderBy(p => p.ActionName);
                    }
                    else
                    {
                        actionitems = actionitems
                            .OrderByDescending(p => p.ActionName);
                    }
                }
                else
                {
                    if (sortDirection == "asc")
                    {
                        actionitems = actionitems
                            .OrderBy(p => p.ActionDescription);
                    }
                    else
                    {
                        actionitems = actionitems
                            .OrderByDescending(p => p.ActionDescription);
                    }
                }
                ViewData["sortField"] = sortField;
                ViewData["sortDirection"] = sortDirection;
                ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

                int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Actions");
                ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
                var pagedData = await PaginatedList<ActionItem>.CreateAsync(actionitems.AsNoTracking(), page ?? 1, pageSize);
                return View(pagedData);
            }
            
        }

        // GET: Actions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.ActionItems == null)
            {
                return NotFound();
            }

            var actionitem = await _context.ActionItems
                .Include(t => t.SupplimentaryDocuments)
                .Include(t => t.Meeting)
                .Include(t => t.Member).ThenInclude(t => t.MemberCommitteeEnrolls).ThenInclude(t => t.Committee)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ActionID == id);
            if (actionitem == null)
            {
                return NotFound();
            }

            return View(actionitem);
        }

        // GET: Actions/Create
        [Authorize(Roles = "Top Admin")]
        public IActionResult Create()
        {
            ViewDataReturnURL();
            DropDownList();
            return View();
        }

        // POST: Actions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Create([Bind("ActionID,ActionName,ActionDescription,ActionDueDate,MeetingID,MemberID,IsCompleted")] ActionItem actionitem)
        {
            ViewDataReturnURL();

            if (ModelState.IsValid)
            {
                _context.Add(actionitem);
                await _context.SaveChangesAsync();
                TempData["Message"] = "You have successfully created an Action Item.";
                return RedirectToAction(nameof(Index));
            }
            DropDownList(actionitem);
            return View(actionitem);
        }

        // GET: Actions/Edit/5
        [Authorize(Roles = "Top Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.ActionItems == null)
            {
                return NotFound();
            }

            var actionitem = await _context.ActionItems
                .Include(m => m.SupplimentaryDocuments)
                .FirstOrDefaultAsync(m => m.ActionID == id);
            if (actionitem == null)
            {
                return NotFound();
            }
            DropDownList(actionitem);
            return View(actionitem);
        }

        // POST: Actions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin,User")]
        public async Task<IActionResult> Edit(int id, Byte[] RowVersion, List<IFormFile> theFiles)
        {
            ViewDataReturnURL();
            var actionitemToUpdate = await _context.ActionItems
                .Include(m => m.SupplimentaryDocuments)
                .FirstOrDefaultAsync(m => m.ActionID == id);

            if (actionitemToUpdate == null)
            {
                return NotFound();
            }
            _context.Entry(actionitemToUpdate).Property("RowVersion").OriginalValue = RowVersion;
            
            if (await TryUpdateModelAsync<ActionItem>(actionitemToUpdate, "",
                t => t.ActionName, t => t.ActionDescription, t => t.ActionDueDate, t => t.MemberID, t => t.MeetingID, t => t.IsCompleted, t => t.ActionNotes))
            {
                try
                {
                    await AddDocumentsAsync(actionitemToUpdate, theFiles);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "You have successfully updated an Action Item.";
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Can't save changes even after multiple attempts. Please try again.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionExists(actionitemToUpdate.ActionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user. Please go back and refresh.");
                    }
                }
            }
            DropDownList(actionitemToUpdate);
            return View(actionitemToUpdate);
        }

        // GET: Actions/Delete/5
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.ActionItems == null)
            {
                return NotFound();
            }

            var actionitem = await _context.ActionItems
                .Include(t => t.Meeting).ThenInclude(t => t.Committee)
                .Include(t => t.Member)
                .FirstOrDefaultAsync(m => m.ActionID == id);
            if (actionitem == null)
            {
                return NotFound();
            }

            return View(actionitem);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActionItems == null)
            {
                return Problem("Entity set 'NC_PAC_Context.Actions'  is null.");
            }
            var actionitem = await _context.ActionItems.FindAsync(id);
            if (actionitem != null)
            {
                _context.ActionItems.Remove(actionitem);
            }
            
            await _context.SaveChangesAsync();
            TempData["Message"] = "You have successfully deleted an Action Item.";
            return RedirectToAction(nameof(Index));
        }
        public void DropDownList(ActionItem actionitem = null)
        {
            ViewData["MeetingID"] = MeetingList(0);
            ViewData["Meeting"] = new SelectList(_context.Meetings.OrderBy(i => i.MeetingTitle), "MeetingID", "MeetingTitle");
            ViewData["MemberID"] = new SelectList(_context.Members.OrderBy(i => i.MemberLastName), "MemberID", "MemFullName");
            ViewData["Member"] = MemberList(0,null);
            ViewData["IsCompleted"] = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Completed", Value = "true" },
                new SelectListItem{ Text="Not Completed", Value = "false" }
            };
        }

        [HttpGet]
        public JsonResult GetMembers(int MeetingID)
        {
            return Json(_context.Members.Where(i => i.MemberCommitteeEnrolls.FirstOrDefault().Committee.CommitteeMeetings.FirstOrDefault().MeetingID == MeetingID));
        }
        private SelectList MeetingList(int selectedmeeting)
        {
            return new SelectList(_context.Meetings.OrderBy(i => i.MeetingTitle), "MeetingID", "MeetingAndDate", selectedmeeting);
        }
        private SelectList MemberList(int MeetingID, int? selectedID)
        {
            var query = from m in _context.Members
                        select m;

            if (MeetingID != 0)
            {
                query = query.Where(i => i.MemberCommitteeEnrolls.FirstOrDefault().Committee.CommitteeMeetings.FirstOrDefault().MeetingID == MeetingID);
            }

            return new SelectList(query.OrderBy(i => i.MemberLastName), "MemberID", "MemFullName", selectedID);
        }
        
        public async Task<FileContentResult> Download(int id)
        {
            var theFile = await _context.UploadedFiles
                .Include(d => d.FileContent)
                .Where(f => f.ID == id)
                .FirstOrDefaultAsync();
            return File(theFile.FileContent.Content, theFile.MimeType, theFile.FileName);
        }

        private async Task AddDocumentsAsync(ActionItem actionitem, List<IFormFile> theFiles)
        {
            // looping through the list of files
            foreach (var f in theFiles)
            {
                if (f != null)
                {
                    string mimeType = f.ContentType;
                    string fileName = Path.GetFileName(f.FileName);
                    long fileLength = f.Length;
                    // checking if have any file to add
                    if (!(fileName == "" || fileLength == 0))
                    {
                        SupplimentaryDocument m = new SupplimentaryDocument();
                        using (var memoryStream = new MemoryStream())
                        {
                            await f.CopyToAsync(memoryStream);
                            m.FileContent.Content = memoryStream.ToArray();
                        }
                        m.MimeType = mimeType;
                        m.FileName = fileName;
                        actionitem.SupplimentaryDocuments.Add(m);
                    };
                }
            }
        }
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }
        private bool ActionExists(int id)
        {
          return _context.ActionItems.Any(e => e.ActionID == id);
        }

    }
}
