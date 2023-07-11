using System;
using System.Collections.Generic;
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

namespace NcPac_Project2.Controllers
{
    [Authorize(Roles = "Top Admin")]
    public class MeetingMinutesController : Controller
    {
        private readonly NC_PAC_Context _context;

        public MeetingMinutesController(NC_PAC_Context context)
        {
            _context = context;
        }

        // GET: MeetingMinutes
        public async Task<IActionResult> Index(string SearchName, string SearchFile,
            int? page, int? pageSizeID, string actionButton, string sortFieldID, string sortDirectionCheck, string sortDirection = "asc", string sortField = "FileName")
        {
            // clearing the cookie
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);
            // Initially assuming the filtering is not going on at the point
            ViewData["FilteringData"] = "filterbuttons";
            // populating dropdownlist for musician filter
            DropDownList();
            // declaring thean array for sorting choices
            string[] sortOptions = new[] { "Meeting Title", "FileName" };

            var minuteContext = _context.MeetingDocuments.Include(m => m.Meeting).AsNoTracking();

            if (!String.IsNullOrEmpty(SearchName))
            {
                minuteContext = minuteContext.Where(p => p.Meeting.MeetingTitle.ToUpper().Contains(SearchName.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (string.IsNullOrEmpty(SearchFile) == false)
            {
                minuteContext = minuteContext.Where(m => m.FileName.ToUpper().Contains(SearchFile.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }

            // Checking whether the filter or sort to perform
            if (String.IsNullOrEmpty(actionButton) == false)
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

            // sorting by file name
            if (sortField == "FileName")
            {
                if (sortDirection == "asc")
                {
                    minuteContext = minuteContext
                        .OrderBy(m => m.FileName);
                }
                else
                {
                    minuteContext = minuteContext
                        .OrderByDescending(m => m.FileName);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    minuteContext = minuteContext
                        .OrderBy(m => m.Meeting.MeetingTitle);
                }
                else
                {
                    minuteContext = minuteContext
                        .OrderByDescending(m => m.Meeting.MeetingTitle);
                }
            }
            // getting ready the sort for the next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Meetings");
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<MeetingDocument>.CreateAsync(minuteContext.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
        }

        // GET: MeetingMinutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.MeetingDocuments == null)
            {
                return NotFound();
            }

            var meetingMinute = await _context.MeetingDocuments
                .Include(m => m.Meeting)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meetingMinute == null)
            {
                return NotFound();
            }

            return View(meetingMinute);
        }

        // GET: MeetingMinutes/Create
        public IActionResult Create()
        {
            ViewDataReturnURL();
            DropDownList();
            return View();
        }

        // POST: MeetingMinutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetingID,Description,ID,FileName,MimeType")] MeetingDocument meetingMinute)
        {
            ViewDataReturnURL();
            if (ModelState.IsValid)
            {
                _context.Add(meetingMinute);
                await _context.SaveChangesAsync();
                TempData["Message"] = "You have successfully created a Minutes document.";
                return RedirectToAction(nameof(Index));
            }
            DropDownList();
            return View(meetingMinute);
        }

        // GET: MeetingMinutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.MeetingDocuments == null)
            {
                return NotFound();
            }

            var meetingMinute = await _context.MeetingDocuments.FindAsync(id);
            if (meetingMinute == null)
            {
                return NotFound();
            }
            ViewData["MeetingID"] = new SelectList(_context.Meetings, "MeetingID", "MeetingTitle", meetingMinute.MeetingID);
            DropDownList(meetingMinute);
            return View(meetingMinute);
        }

        // POST: MeetingMinutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var minute = await _context.MeetingDocuments.Include(m => m.Meeting).FirstOrDefaultAsync(m => m.ID == id);
            if (minute == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<MeetingDocument>(minute, "",
                m => m.MeetingID, m => m.Description))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "You have successfully updated a Minutes document.";
                    return RedirectToAction("Details", new { minute.ID });
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Can't save changes even after multiple attempts. Please try again.");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            ViewData["MeetingID"] = new SelectList(_context.Meetings, "MeetingID", "MeetingTitle", minute.MeetingID);
            DropDownList(minute);
            return View(minute);
        }

        // GET: MeetingMinutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeetingDocuments == null)
            {
                return NotFound();
            }

            var meetingMinute = await _context.MeetingDocuments
                .Include(m => m.Meeting)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meetingMinute == null)
            {
                return NotFound();
            }

            return View(meetingMinute);
        }

        // POST: MeetingMinutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeetingDocuments == null)
            {
                return Problem("Entity set 'NC_PAC_Context.MeetingMinutes'  is null.");
            }
            var meetingMinute = await _context.MeetingDocuments.FindAsync(id);
            if (meetingMinute != null)
            {
                _context.MeetingDocuments.Remove(meetingMinute);
            }
            
            await _context.SaveChangesAsync();
            TempData["Message"] = "You have successfully deleted a Minutes document.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<FileContentResult> Download(int id)
        {
            var theFile = await _context.UploadedFiles
                .Include(d => d.FileContent)
                .Where(f => f.ID == id)
                .FirstOrDefaultAsync();
            return File(theFile.FileContent.Content, theFile.MimeType, theFile.FileName);
        }
        // public method to display dropdown list values in alphabatical order 
        public void DropDownList(MeetingDocument minute = null)
        {
            ViewData["Meeting"] = new SelectList(_context.Meetings.OrderBy(i => i.MeetingTitle), "MeetingID", "MeetingTitle");
        }
        // method to get controller name
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        // method to return the url of the controller
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }
        private bool MeetingMinuteExists(int id)
        {
          return _context.MeetingDocuments.Any(e => e.ID == id);
        }
    }
}
