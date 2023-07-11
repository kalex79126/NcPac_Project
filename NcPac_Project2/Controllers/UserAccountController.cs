using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NcPac_Project2.Utilities;
using System.Diagnostics.Metrics;

namespace NcPac_Project2.Controllers
{
    [Authorize]
    public class UserAccountController : Controller
    {
        private readonly NC_PAC_Context _context;

        public UserAccountController(NC_PAC_Context context)
        {
            _context = context;
        }

        // GET: UserAccount
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Details));
        }

        // GET: UserAccount/Details/5
        public async Task<IActionResult> Details()
        {
            var user = await _context.Members
                .Include(e => e.MemberCommitteeEnrolls).ThenInclude(e => e.Committee).ThenInclude(e => e.Division)
               .Where(e => e.MemberEmail == User.Identity.Name)
               .Select(e => new Member
               {
                   Salutation = e.Salutation,
                   MemberID = e.MemberID,
                   MemberFirstName = e.MemberFirstName,
                   MemberLastName = e.MemberLastName,
                   MemberAddress = e.MemberAddress,
                   MemberCity = e.MemberCity,
                   MemberProvince = e.MemberProvince,
                   MemberPostalCode = e.MemberPostalCode,
                   MemberPhoneNo = e.MemberPhoneNo,
                   MemberEmail = e.MemberEmail,
                   MemberPosition = e.MemberPosition,
                   MemberCompany = e.MemberCompany,
                   MemberEmAddress = e.MemberEmAddress,
                   MemberEmCity = e.MemberEmCity,
                   MemberEmProvince = e.MemberEmProvince,
                   MemberEmPostalCode = e.MemberEmPostalCode,
                   MemberEmPhoneNo = e.MemberEmPhoneNo,
                   MemberEmEmail = e.MemberEmEmail,
                   MemberIsMailingHome = e.MemberIsMailingHome,
                   MemberIsNcGrad = e.MemberIsNcGrad,
                   MemberEducation = e.MemberEducation,
                   MemberParticipation = e.MemberParticipation,
                   MemberCommitteeEnrolls = e.MemberCommitteeEnrolls,
               })
               .AsNoTracking()
               .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UserAccount/Edit/5
        public async Task<IActionResult> Edit()
        {
            var user = await _context.Members
               .Where(e => e.MemberEmail == User.Identity.Name)
               .Select(e => new Member
               {
                   Salutation = e.Salutation,
                   MemberID = e.MemberID,
                   MemberFirstName = e.MemberFirstName,
                   MemberLastName = e.MemberLastName,
                   MemberAddress = e.MemberAddress,
                   MemberCity = e.MemberCity,
                   MemberProvince = e.MemberProvince,
                   MemberPostalCode = e.MemberPostalCode,
                   MemberPhoneNo = e.MemberPhoneNo,
                   MemberEmail = e.MemberEmail,
                   MemberPosition = e.MemberPosition,
                   MemberCompany = e.MemberCompany,
                   MemberEmAddress = e.MemberEmAddress,
                   MemberEmCity = e.MemberEmCity,
                   MemberEmProvince = e.MemberEmProvince,
                   MemberEmPostalCode = e.MemberEmPostalCode,
                   MemberEmPhoneNo = e.MemberEmPhoneNo,
                   MemberEmEmail = e.MemberEmEmail,
                   MemberIsMailingHome = e.MemberIsMailingHome,
                   MemberIsNcGrad = e.MemberIsNcGrad,
                   MemberEducation = e.MemberEducation,
                   MemberParticipation = e.MemberParticipation,
               })
               .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            DropDownList(user);
            return View(user);
        }

        // POST: UserAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var userToUpdate = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);

            //Note: Using TryUpdateModel we do not need to invoke the ViewModel
            //Only allow some properties to be updated
            if (await TryUpdateModelAsync<Member>(userToUpdate, "",
                m => m.Salutation, m => m.MemberFirstName, m => m.MemberLastName, m => m.MemberAddress, m => m.MemberCity, m => m.MemberProvince, m => m.MemberPostalCode,
                m => m.MemberPhoneNo, m => m.MemberEmail, m => m.MemberPosition, m => m.MemberCompany, m => m.MemberPosition, m => m.MemberEmAddress, m => m.MemberEmCity, m => m.MemberEmProvince, m => m.MemberEmPostalCode,
                m => m.MemberEmPhoneNo, m => m.MemberEmEmail, m => m.MemberIsMailingHome, m => m.MemberIsNcGrad, m => m.MemberEducation, m => m.MemberParticipation))
            {
                try
                {
                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(userToUpdate.MemFullName);
                    TempData["Message"] = "You have successfully updated your details.";
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.MemberID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    //Since we do not allow changing the email, we cannot introduce a duplicate
                    ModelState.AddModelError("", "Something went wrong in the database.");
                }
            }
            DropDownList(userToUpdate);
            return View(userToUpdate);
        }
        private void UpdateUserNameCookie(string userName)
        {
            CookieHelper.CookieSet(HttpContext, "userName", userName, 960);
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
        private bool UserExists(int id)
        {
            return _context.Members.Any(e => e.MemberID == id);
        }
    }
}
