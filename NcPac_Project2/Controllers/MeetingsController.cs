using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NcPac_Project2.Utilities;
using System.Diagnostics.Metrics;

namespace NcPac_Project2.Controllers
{
    [Authorize]
    public class MeetingsController : Controller
    {
        private readonly NC_PAC_Context _context;

        public MeetingsController(NC_PAC_Context context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index(int? page, int? pageSizeID, int? Committee, string SearchName, string SearchStartDate, string SearchEndDate, string actionButton, string sortFieldID, string SearchRenewalDate, string sortDirectionCheck, string sortDirection = "asc", string sortField = "Meeting Title")
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);
            ViewData["FilteringData"] = "filterbuttons";

            DropDownList();

            string[] sortOptions = new[] { "Meeting Title", "Meeting Date", "Committee Title" };

            var meetings = _context.Meetings
                .Include(m => m.MeetingMinutes)
                .Include(m => m.CommitteeMeetings).ThenInclude(m => m.Committee)
                
                .AsNoTracking();

            if (Committee.HasValue)
            {
                meetings = meetings.Where(m => m.CommitteeMeetings.FirstOrDefault().Committee.CommitteeID == Committee);
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchName))
            {
                meetings = meetings.Where(p => p.MeetingTitle.ToUpper().Contains(SearchName.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchStartDate) && !String.IsNullOrEmpty(SearchEndDate))
            {
                if (DateTime.Parse(SearchEndDate) < DateTime.Parse(SearchStartDate))
                {
                    ModelState.AddModelError(string.Empty, "Start date can't be before End date");
                }
                else
                {
                    meetings = meetings.Where(p => p.MeetingDate >= DateTime.Parse(SearchStartDate) && p.MeetingDate <= DateTime.Parse(SearchEndDate));
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
            if (sortField == "Meeting Title")
            {
                if (sortDirection == "asc")
                {
                    meetings = meetings
                        .OrderBy(p => p.MeetingTitle);
                }
                else
                {
                    meetings = meetings
                        .OrderByDescending(p => p.MeetingTitle);
                }
            }
            else if (sortField == "Committee Title")
            {
                if (sortDirection == "asc")
                {
                    meetings = meetings
                        .OrderBy(p => p.Committee.CommitteeName);
                }
                else
                {
                    meetings = meetings
                        .OrderByDescending(p => p.Committee.CommitteeName);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    meetings = meetings
                        .OrderBy(p => p.MeetingDate);
                }
                else
                {
                    meetings = meetings
                        .OrderByDescending(p => p.MeetingDate);
                }
            }
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Meetings");
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Meeting>.CreateAsync(meetings.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .Include(m => m.MeetingMinutes)
                .Include(m => m.Attendances).ThenInclude(m => m.Member)
                .Include(m => m.Committee)
                .Include(m => m.ActionItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        [Authorize(Roles = "Top Admin")]
        public IActionResult Create()
        {
            ViewDataReturnURL();
            ViewData["CommitteeID"] = new SelectList(_context.Committees.OrderBy(c => c.CommitteeName), "CommitteeID", "CommitteeName");
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Create([Bind("MeetingID,MeetingTitle,MeetingDate,MeetingNotes,CommitteeID")] Meeting meeting, List<IFormFile> theFiles)
        {
            ViewDataReturnURL();
            if (ModelState.IsValid)
            {
                await AddDocumentsAsync(meeting, theFiles);
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                TempData["Message"] = "You have successfully created a Meeting.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommitteeID"] = new SelectList(_context.Committees.OrderBy(c => c.CommitteeName), "CommitteeID", "CommitteeName", meeting.CommitteeID);
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .Include(m => m.MeetingMinutes)
                .FirstOrDefaultAsync(m => m.MeetingID == id);

            if (meeting == null)
            {
                return NotFound();
            }
            ViewData["CommitteeID"] = new SelectList(_context.Committees.OrderBy(c => c.CommitteeName), "CommitteeID", "CommitteeName", meeting.CommitteeID);
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Edit(int id, Byte[] RowVersion, List<IFormFile> theFiles)
        {
            ViewDataReturnURL();
            var meetingToUpdate = await _context.Meetings
                .Include(m => m.MeetingMinutes)
                .FirstOrDefaultAsync(m => m.MeetingID == id);

            if (meetingToUpdate == null)
            {
                return NotFound();
            }
            _context.Entry(meetingToUpdate).Property("RowVersion").OriginalValue = RowVersion;

            if (await TryUpdateModelAsync<Meeting>(meetingToUpdate, "", m => m.MeetingNotes, m => m.MeetingTitle, m => m.CommitteeID, m => m.MeetingDate))
            {
                try
                {
                    await AddDocumentsAsync(meetingToUpdate, theFiles);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "You have successfully updated a Meeting Details.";
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Can't save changes even after multiple attempts. Please try again.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meetingToUpdate.MeetingID))
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
            ViewData["CommitteeID"] = new SelectList(_context.Committees.OrderBy(c => c.CommitteeName), "CommitteeID", "CommitteeName", meetingToUpdate.CommitteeID);
            return View(meetingToUpdate);
        }

        // GET: Meetings/Delete/5
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .Include(m => m.Committee)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewDataReturnURL();
            if (_context.Meetings == null)
            {
                return Problem("Entity set 'NC_PAC_Context.Meetings'  is null.");
            }
            var meeting = await _context.Meetings
                .FirstOrDefaultAsync(m => m.MeetingID == id);
            try
            {
                if (meeting != null)
                {
                    _context.Meetings.Remove(meeting);
                }
                await _context.SaveChangesAsync();
                TempData["Message"] = "You have successfully deleted a Meeting.";
                return Redirect(ViewData["returnURL"].ToString());
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Meeting. Remember, you cannot delete an Meeting that assigned to other entities in the database.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(meeting);
        }

        public void DropDownList(Meeting meeting = null)
        {
            ViewData["Committee"] = new SelectList(_context.Committees.OrderBy(i => i.CommitteeName), "CommitteeID", "CommitteeName");
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }
        public async Task<FileContentResult> Download(int id)
        {
            var theFile = await _context.UploadedFiles
                .Include(d => d.FileContent)
                .Where(f => f.ID == id)
                .FirstOrDefaultAsync();
            return File(theFile.FileContent.Content, theFile.MimeType, theFile.FileName);
        }
        private async System.Threading.Tasks.Task AddDocumentsAsync(Meeting meeting, List<IFormFile> theFiles)
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
                        MeetingDocument m = new MeetingDocument();
                        using (var memoryStream = new MemoryStream())
                        {
                            await f.CopyToAsync(memoryStream);
                            m.FileContent.Content = memoryStream.ToArray();
                        }
                        m.MimeType = mimeType;
                        m.FileName = fileName;
                        meeting.MeetingMinutes.Add(m);
                    };
                }
            }
        }
        private bool MeetingExists(int id)
        {
          return _context.Meetings.Any(e => e.MeetingID == id);
        }
    }
}
