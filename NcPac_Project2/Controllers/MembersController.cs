using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Design;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NcPac_Project2.Utilities;
using NcPac_Project2.ViewModels;
using NuGet.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.Style;
using System.Data;
using System.Net.Mail;
using Org.BouncyCastle.Crypto.Tls;
using Microsoft.Build.Framework;

namespace NcPac_Project2.Controllers
{
    public class MembersController : Controller
    {
        private readonly NC_PAC_Context _context;

        public MembersController(NC_PAC_Context context)
        {
            _context = context;
        }

        //Action for new Register view
        public ActionResult Register()
        {
            return View();
        }

        // GET: Members
        [Authorize(Roles = "Top Admin,Admin")]
        public async Task<IActionResult> Index(int? page, int? pageSizeID, int? Committee, string SearchLast, string SearchEmail, string SearchCompany, string actionButton, string sortFieldID, string SearchStartDate, string SearchEndDate, string sortDirectionCheck, string sortDirection = "asc", string sortField = "Name")
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);
            ViewData["FilteringData"] = "filterbuttons";

            DropDownList();

            string[] sortOptions = new[] { "Name" };

            var members = _context.Members
                .Include(e => e.ActionItems)
                .Include(e => e.Attendances)
                .Include(e => e.MemberCommitteeEnrolls).ThenInclude(e => e.Committee).ThenInclude(e => e.Division)
                .AsNoTracking();

            if (Committee.HasValue)
            {
                members = members.Where(m => m.MemberCommitteeEnrolls.Any(i => i.CommitteeID == Committee));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchLast))
            {
                members = members.Where(p => p.MemberLastName.ToUpper().Contains(SearchLast.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchEmail))
            {
                members = members.Where(p => p.MemberEmail.ToUpper().Contains(SearchEmail.ToUpper())
                                            || p.MemberEmEmail.ToUpper().Contains(SearchEmail.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchCompany))
            {
                members = members.Where(p => p.MemberCompany.ToUpper().Contains(SearchCompany.ToUpper()));
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
                    members = members.Where(p => p.MemberRenewalDate >= DateTime.Parse(SearchStartDate) && p.MemberRenewalDate <= DateTime.Parse(SearchEndDate));
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
                    members = members
                        .OrderBy(p => p.MemberFirstName);
                }
                else
                {
                    members = members
                        .OrderByDescending(p => p.MemberFirstName);
                }
            }
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Members");
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Member>.CreateAsync(members.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
        }

        // GET: Members/Details/5
        [Authorize(Roles = "Top Admin,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(e => e.MemberCommitteeEnrolls).ThenInclude(e => e.Committee).ThenInclude(e => e.Division)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        [Authorize(Roles = "Top Admin")]
        public IActionResult Create()
        {
            ViewDataReturnURL();
            var member = new Member();
            CommitteeData(member);
            DropDownList();
            return View();
        }

        [Authorize(Roles = "Top Admin")]
        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,MemberFirstName,MemberLastName,MemberAddress,MemberCity,MemberProvince,MemberPostalCode,MemberPhoneNo,MemberEmail,MemberPosition,MemberCompany,MemberEmAddress,MemberEmCity,MemberEmProvince,MemberEmPostalCode,MemberEmPhoneNo,MemberEmEmail,MemberIsMailingHome,MemberIsNcGrad,MemberEducation,MemberOccupationalSummary,MemberRegisteredAt,MemberLastLogin,MemberRenewalDate")] Member member, string[] selectedCommittees)
        {
            ViewDataReturnURL();
            try
            {
                if (selectedCommittees != null)
                {
                    foreach (var comm in selectedCommittees)
                    {
                        var addComm = new MemberCommitteeEnroll { MemberID = member.MemberID, CommitteeID = int.Parse(comm) };
                        member.MemberCommitteeEnrolls.Add(addComm);
                    }
                }

                if (ModelState.IsValid)
                {
                    _context.Add(member);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "You have successfully created a Member.";
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Can't save changes even after multiple attempts. Please try again.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.GetBaseException().Message.Contains("UNIQUE constraint failed: Members.MemberEmail"))
                {
                    ModelState.AddModelError("MemberEmail", "Member Email should be unique, can't save changes");
                }
                else
                {
                    ModelState.AddModelError("", "Can't save changes. Please try again");
                }
            }
            CommitteeData(member);
            DropDownList(member);
            return View(member);
        }
        
        
        //Action for Register Page
        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("MemberID,MemberFirstName,MemberLastName,MemberAddress," +
            "MemberCity,MemberProvince,MemberPostalCode,MemberPhoneNo,MemberEmail,MemberPosition,MemberCompany," +
            "MemberEmAddress,MemberEmCity,MemberEmProvince,MemberEmPostalCode,MemberEmPhoneNo,MemberEmEmail," +
            "MemberIsMailingHome,MemberIsNcGrad,MemberEducation,MemberOccupationalSummary,MemberRegisteredAt," +
            "MemberLastLogin,MemberRenewalDate,MemberIsActive")] Member member)
        {

            ViewDataReturnURL();
            try 
            {
                if (ModelState.IsValid)
                {
                    _context.Add(member);
                    await _context.SaveChangesAsync();
                    return View("RegisterConfirmation");
                }
            }
            catch (DbUpdateException dex) //To catch duplicate Email
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                {
                    ModelState.AddModelError("Email", "Unable to save changes. There is already an account with this Email address.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to send the form. Try again, and if the problem persists please send us an email to email@email.com");
                }
            }
            
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(e => e.MemberCommitteeEnrolls).ThenInclude(e => e.Committee)
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }
            CommitteeData(member);
            DropDownList(member);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Edit(int id, string[] selectedCommittees, Byte[] RowVersion)
        {
            ViewDataReturnURL();
            var memberToUpdate = await _context.Members
                .Include(e => e.MemberCommitteeEnrolls).ThenInclude(e => e.Committee)
                .FirstOrDefaultAsync(m => m.MemberID == id);

            if (memberToUpdate == null)
            {
                return NotFound();
            }
            
            UpdateEnrolls(selectedCommittees, memberToUpdate);

            _context.Entry(memberToUpdate).Property("RowVersion").OriginalValue = RowVersion;

            if (await TryUpdateModelAsync<Member>(memberToUpdate, "",
                m => m.Salutation, m => m.MemberFirstName, m => m.MemberLastName, m => m.MemberAddress, m => m.MemberCity,
                m => m.MemberProvince, m => m.MemberPostalCode,m => m.MemberPhoneNo,
                m => m.MemberEmail, m => m.MemberPosition, m => m.MemberCompany,
                m => m.MemberEmAddress, m => m.MemberEmCity, m => m.MemberEmProvince, m => m.MemberEmPostalCode,
                m => m.MemberEmPhoneNo, m => m.MemberEmEmail, m => m.MemberIsMailingHome,
                m => m.MemberIsNcGrad, m => m.MemberEducation, m => m.MemberParticipation,
                m => m.MemberRegisteredAt, m => m.MemberLastLogin, m => m.MemberRenewalDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "You have successfully updated a Member Details.";
                    return Redirect(ViewData["returnURL"].ToString());

                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Can't save changes even after multiple attempts. Please try again.");
                }
                // for concurrency control
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(memberToUpdate.MemberID))
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
            CommitteeData(memberToUpdate);
            DropDownList(memberToUpdate);
            return View(memberToUpdate);
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewDataReturnURL();
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewDataReturnURL();
            if (_context.Members == null)
            {
                return Problem("Entity set 'NC_PAC_Context.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "You have successfully deleted a Member.";
            return Redirect(ViewData["returnURL"].ToString());
        }

        [Authorize(Roles = "Top Admin")]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
            ExcelPackage excel;

            if (theExcel != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await theExcel.CopyToAsync(memoryStream);
                    excel = new ExcelPackage(memoryStream);
                }
                var workSheet = excel.Workbook.Worksheets[0];
                var start = workSheet.Row(4);
                var end = workSheet.Dimension.End;
                List<Member> MemberNames = new List<Member>();
                for (int row = start.Row; row <= end.Row; row++)
                {
                    Member i = new Member
                    {
                        MemberFirstName = workSheet.Cells[row, 1].Text,
                        MemberLastName = workSheet.Cells[row, 2].Text,
                        MemberAddress = workSheet.Cells[row, 3].Text,
                        MemberCity = workSheet.Cells[row, 4].Text,
                        MemberProvince = workSheet.Cells[row, 5].Text,
                        MemberPostalCode = workSheet.Cells[row, 6].Text,
                        MemberPhoneNo = workSheet.Cells[row, 7].Text,
                        MemberEmail = workSheet.Cells[row, 8].Text,
                        MemberPosition = workSheet.Cells[row, 9].Text,
                        MemberCompany = workSheet.Cells[row, 10].Text,
                        MemberEmAddress = workSheet.Cells[row, 11].Text,
                        MemberEmCity = workSheet.Cells[row, 12].Text,
                        MemberEmProvince = workSheet.Cells[row, 13].Text,
                        MemberEmPostalCode = workSheet.Cells[row, 14].Text,
                        MemberEmPhoneNo = workSheet.Cells[row, 15].Text,
                        MemberEmEmail = workSheet.Cells[row, 16].Text,
                        MemberIsMailingHome = workSheet.Cells[row, 17].Text,
                        MemberIsNcGrad = workSheet.Cells[row, 18].Text,
                        MemberEducation = workSheet.Cells[row, 19].Text,
                        MemberParticipation = workSheet.Cells[row, 20].Text,
                        MemberRegisteredAt = DateTime.Parse(workSheet.Cells[row, 21].Text),
                        MemberLastLogin = DateTime.Parse(workSheet.Cells[row, 22].Text),
                        MemberRenewalDate = DateTime.Parse(workSheet.Cells[row, 23].Text)
                    };
                    MemberNames.Add(i);
                }
                try
                {
                    _context.Members.AddRange(MemberNames);
                    _context.SaveChanges();
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (DbUpdateException dex) //To catch duplicate Email
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                    {
                        TempData["ErrorMessage"] = "Unable to save changes. There is already an account with this Email address.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to send the form. Try again, and if the problem persists please send us an email to email@email.com";
                    }
                }

            }
            else
            {
                ViewBag.Message = "You didn't select a file";
            }
            return RedirectToAction("Index", new { Tab = "Members-Tab" });
        }

        public void DropDownList(Member member = null)
        {
            ViewData["MemberProvinces"] = new SelectList(new[]
            {
                new { Value="AB", Text="Alberta"},
                new { Value="BC", Text="British Columbia" },
                new { Value="MB", Text="Manitoba" },
                new { Value="NB", Text="New Brunswick"},
                new { Value="NL", Text="Newfoundland and Labrador"},
                new { Value="NT", Text="Northwest Territories"},
                new { Value="NS", Text="Nova Scotia"},
                new { Value="NU", Text="Nunavut"},
                new { Value="ON", Text="Ontario"},
                new { Value="PE", Text="Prince Edward Island"},
                new { Value="QC", Text="Quebec"},
                new { Value="SK", Text="Saskatchewan"},
                new { Value="YT", Text="Yukon"},
            }, "Text", "Text", member?.MemberProvince);

            ViewData["EmpProvinces"] = new SelectList(new[]
            {
                new { Value="AB", Text="Alberta"},
                new { Value="BC", Text="British Columbia" },
                new { Value="MB", Text="Manitoba" },
                new { Value="NB", Text="New Brunswick"},
                new { Value="NL", Text="Newfoundland and Labrador"},
                new { Value="NT", Text="Northwest Territories"},
                new { Value="NS", Text="Nova Scotia"},
                new { Value="NU", Text="Nunavut"},
                new { Value="ON", Text="Ontario"},
                new { Value="PE", Text="Prince Edward Island"},
                new { Value="QC", Text="Quebec"},
                new { Value="SK", Text="Saskatchewan"},
                new { Value="YT", Text="Yukon"}
            }, "Text", "Text", member?.MemberEmProvince);

            ViewData["Committee"] = new SelectList(_context.Committees.OrderBy(i => i.CommitteeName), "CommitteeID", "CommitteeName");

            ViewData["Division"] = new SelectList(_context.Divisions.OrderBy(i => i.DivisionTitle), "DivisionID", "DivisionTitle");

        }

        [Authorize(Roles = "Top Admin")]
        public IActionResult DownloadTemplate()
        {
            var mems = _context.Members.Where(m => m.MemberID == 1).Select(grp => new
            {
                FirstName = grp.MemberFirstName,
                LastName = grp.MemberLastName,
                Address = grp.MemberAddress,
                City = grp.MemberCity,
                Province = grp.MemberProvince,
                PostalCode = grp.MemberPostalCode,
                Phone = grp.MemberPhoneNo,
                Email = grp.MemberEmail,
                Position = grp.MemberPosition,
                Company = grp.MemberCompany,
                EmpAddress = grp.MemberEmAddress,
                EmpCity = grp.MemberEmCity,
                EmpProvince = grp.MemberEmProvince,
                EmpPostalCode = grp.MemberEmPostalCode,
                EmpPhone = grp.MemberEmPhoneNo,
                EmpEmail = grp.MemberEmEmail,
                PreferedEmail = grp.MemberIsMailingHome,
                IsNCGrad = grp.MemberIsNcGrad,
                Education = grp.MemberEducation,
                Participation = grp.MemberParticipation,
                RegisterDate = grp.MemberRegisteredAt.ToString(),
                LastLoginDate = grp.MemberLastLogin.ToString(),
                RenewalDate = grp.MemberRenewalDate.ToString()
            });

            int numRows = mems.Count();

            if (numRows > 0)
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var workSheet = excel.Workbook.Worksheets.Add("Performances");

                    workSheet.Cells[3, 1].LoadFromCollection(mems, true);

                    //Set Style and backgound colour of headings
                    using (ExcelRange headings = workSheet.Cells[3, 1, 3, 23])
                    {
                        headings.Style.Font.Bold = true;
                        var fill = headings.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.Aqua);
                    }

                    //Autofit columns
                    workSheet.Cells.AutoFitColumns();

                    workSheet.Cells[1, 1].Value = "Template to Add Member through Excel Sheet";
                    using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 23])
                    {
                        Rng.Merge = true;
                        Rng.Style.Font.Bold = true;
                        Rng.Style.Font.Size = 18;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    DateTime utcDate = DateTime.UtcNow;
                    TimeZoneInfo esTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, esTimeZone);
                    using (ExcelRange Rng = workSheet.Cells[2, 10])
                    {
                        Rng.Value = "Downloaded at: " + localDate.ToShortTimeString() + " on " +
                            localDate.ToShortDateString();
                        Rng.Style.Font.Bold = true;
                        Rng.Style.Font.Size = 12;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }

                    try
                    {
                        Byte[] theData = excel.GetAsByteArray();
                        string filename = "Template Sheet.xlsx";
                        string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        return File(theData, mimeType, filename);
                    }
                    catch (Exception)
                    {
                        return BadRequest("Can't download the file.");
                    }
                }
            }
            return NotFound("No records found.");
        }

        public async Task<IActionResult> MemberSummaryAsync(int? page, int? pageSizeID, int? Committee, string SearchLast, string SearchEmail, string SearchCompany, string actionButton, string sortFieldID, string SearchStartDate, string SearchEndDate, string sortDirectionCheck, string sortDirection = "asc", string sortField = "First Name")
        {
            string[] sortOptions = new[] { "First Name", "Last Name" };
            ViewData["FilteringData"] = "filterbuttons";

            var sumQ = _context.Members.Include(m => m.MemberCommitteeEnrolls)
                        .ThenInclude(e => e.Committee)
                        .ThenInclude(c => c.Division)
                        .Include(a => a.Attendances)
                        .Select(m => new MemberSummaryVM
            {
                ID = m.MemberID,
                FirstName = m.MemberFirstName,
                LastName = m.MemberLastName,
                City = m.MemberCity,
                Address = m.MemberAddress,
                Email = m.MemberEmail,
                Phone = m.MemberPhoneNo,
                JobTitle = m.MemberPosition,
                Company = m.MemberCompany,
                NcGrad = m.MemberIsNcGrad,
                RenewedDate = m.MemberRenewalDate,
                RegisteredAt = m.MemberRegisteredAt.Value,
                PacChairs = m.MemberCommitteeEnrolls.Count().ToString(),
                CommitteeName = m.MemberCommitteeEnrolls.FirstOrDefault(e => e.Committee != null) != null ? m.MemberCommitteeEnrolls.FirstOrDefault(e => e.Committee != null).Committee.CommitteeName : "No Committee Assigned",
                DivisionName = m.MemberCommitteeEnrolls.FirstOrDefault(e => e.Committee != null) != null ? m.MemberCommitteeEnrolls.FirstOrDefault(e => e.Committee != null).Committee.Division.DivisionTitle : "No Division Assigned",
                LastAttendanceDate = m.Attendances
                .Where(a => a.MemberID == m.MemberID && (a.AttendanceFirstRecent || a.AttendanceSecondRecent || a.AttendanceThirdRecent))
                .Max(a => a.Meeting.MeetingDate) //Check if the Member attendences was true in any of the attendances properties
                        })
                .OrderBy(o => o.FirstName)
                .ThenBy(o => o.LastName)
                .AsNoTracking();

            if (Committee.HasValue)
            {
                sumQ = sumQ.Where(m => m.MemberCommitteeEnrolls.Any(i => i.CommitteeID == Committee));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchLast))
            {
                sumQ = sumQ.Where(p => p.LastName.ToUpper().Contains(SearchLast.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchEmail))
            {
                sumQ = sumQ.Where(p => p.Email.ToUpper().Contains(SearchEmail.ToUpper()));
                ViewData["FilteringData"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchCompany))
            {
                sumQ = sumQ.Where(p => p.Company.ToUpper().Contains(SearchCompany.ToUpper()));
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
                    sumQ = sumQ.Where(p => p.RenewedDate >= DateTime.Parse(SearchStartDate) && p.RenewedDate <= DateTime.Parse(SearchEndDate));
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
            if (sortField == "First Name")
            {
                if (sortDirection == "asc")
                {
                    sumQ = sumQ
                        .OrderBy(p => p.FirstName);
                }
                else
                {
                    sumQ = sumQ
                        .OrderByDescending(p => p.FirstName);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    sumQ = sumQ
                        .OrderBy(p => p.LastName);
                }
                else
                {
                    sumQ = sumQ
                        .OrderByDescending(p => p.LastName);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            return View(await sumQ.ToListAsync());
        }

        private void CommitteeData(Member member)
        {
            var committees = _context.Committees;
            var committeesID = new HashSet<int>(member.MemberCommitteeEnrolls.Select(p => p.CommitteeID));
            // List of check box
            var checkBox = new List<CheckBoxViewModel>();
            // looping through database 
            foreach (var comm in committees)
            {
                checkBox.Add(new CheckBoxViewModel
                {
                    ID = comm.CommitteeID,
                    Text = comm.CommitteeName,
                    IsAssigned = committeesID.Contains(comm.CommitteeID)
                });
            }
            ViewData["CommitteeList"] = checkBox;
        }
        private void UpdateEnrolls(string[] selectedCommittees, Member memberToUpdate)
        {
            if (selectedCommittees == null)
            {
                memberToUpdate.MemberCommitteeEnrolls = new List<MemberCommitteeEnroll>();
                return;
            }

            var selectedcommittee = new HashSet<string>(selectedCommittees);
            var memberCommittees = new HashSet<int>
                (memberToUpdate.MemberCommitteeEnrolls.Select(p => p.CommitteeID));
            foreach (var comm in _context.Committees)
            {
                if (selectedcommittee.Contains(comm.CommitteeID.ToString()))
                {
                    if (!memberCommittees.Contains(comm.CommitteeID))
                    {
                        memberToUpdate.MemberCommitteeEnrolls.Add(new MemberCommitteeEnroll { MemberID = memberToUpdate.MemberID, CommitteeID = comm.CommitteeID });
                    }
                }
                else
                {
                    if (memberCommittees.Contains(comm.CommitteeID))
                    {
                        MemberCommitteeEnroll removeCommittee = memberToUpdate.MemberCommitteeEnrolls.SingleOrDefault(p => p.CommitteeID == comm.CommitteeID);
                        _context.Remove(removeCommittee);
                    }
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

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberID == id);
        }
    }
}
