using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NcPac_Project2.Utilities;
using NcPac_Project2.ViewModels;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;

namespace NcPac_Project2.Controllers
{
    [Authorize(Roles = "Top Admin")]
    public class VolunteersController : Controller
    {
        private readonly NC_PAC_Context _context;
        private readonly ApplicationDbContext _identityContext;
        private readonly IMyEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;


        public VolunteersController(NC_PAC_Context context,ApplicationDbContext identityContext, IMyEmailSender emailSender,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _identityContext = identityContext;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index()
        {
              var volunteers = await _context.Accounts
                .Include(v => v.Subscriptions)
                .Select(v => new VolunteerAdminVM
                {
                    Phone = v.Phone,
                    Email = v.Email,
                    Active = v.Active,
                    ID = v.ID,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    NumberOfPushSubscriptions = v.Subscriptions.Count()
                }).ToListAsync();
            
            foreach (var e in volunteers)
            {
                var user = await _userManager.FindByEmailAsync(e.Email);
                if (user != null)
                {
                    e.UserRoles = (List<string>)await _userManager.GetRolesAsync(user);
                }
            };

            return View(volunteers);

        }

        

        // GET: Volunteers/Create
        public IActionResult Create()
        {
            VolunteerAdminVM volunteer = new VolunteerAdminVM();
            PopulateAssignedRoleData(volunteer);
            return View(volunteer);
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Phone,Email")] Account volunteer, 
            string[] selectedRoles)
        {
            try
            {
                //var user = CreateUser();
                if (ModelState.IsValid)
                {
                    //user.EmailConfirmed = true;
                    _context.Add(volunteer);
                    await _context.SaveChangesAsync();
                    InsertIdentityUser(volunteer.Email, selectedRoles);
                    await InviteUserToResetPassword(volunteer, null);
                    TempData["Message"] = "You have successfully created a Account.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE contraint failed"))
                {
                    ModelState.AddModelError("Email", "Unable to save changes.Remember, you cannot have duplicate Email addresses.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            //We are here because something went wrong and need to redisplay
            VolunteerAdminVM volunteerAdminVM = new VolunteerAdminVM
            {
                Phone = volunteer.Phone,
                Email = volunteer.Email,
                Active = volunteer.Active,
                ID = volunteer.ID,
                FirstName = volunteer.FirstName,
                LastName = volunteer.LastName,
            };            
            foreach (var role in selectedRoles)
            {
                volunteerAdminVM.UserRoles.Add(role);
            }

            PopulateAssignedRoleData(volunteerAdminVM);
            return View(volunteerAdminVM);

        }


        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Accounts
                .Where(v => v.ID == id)
                .Select(v => new VolunteerAdminVM
                {
                    Phone = v.Phone,
                    Email = v.Email,
                    Active = v.Active,
                    ID = v.ID,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                }).FirstOrDefaultAsync();
            
                
            if (volunteer == null)
            {
                return NotFound();
            }
            //Get the user from the Identity system
            var user = await _userManager.FindByEmailAsync(volunteer.Email);
            if (user != null)
            {
                //Add the current roles
                var r = await _userManager.GetRolesAsync(user);
                volunteer.UserRoles = (List<string>)r;
            }

            PopulateAssignedRoleData(volunteer);
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool Active, bool CompletedForm,string[] selectedRoles)
        {
            var volunteerToUpdate = await _context.Accounts
                .FirstOrDefaultAsync(v => v.ID == id);
            if (volunteerToUpdate == null)
            {
                return NotFound();
            }

            //Current Emaiul and Active Status
            bool ActiveStatus = volunteerToUpdate.Active;
            string databaseEmail = volunteerToUpdate.Email;

            if (await TryUpdateModelAsync<Account>(volunteerToUpdate, "",
                v => v.FirstName, v => v.LastName, v => v.Email, v => v.Active, v => v.Phone,v => v.RoleID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    //Save successful so go on to related changes

                    //Check for changes in the Active state
                    if (volunteerToUpdate.Active == false && ActiveStatus == true)
                    {
                        //Deactivating them so delete the IdentityUser
                        //This deletes the user's login from the security system
                        await DeleteIdentityUser(volunteerToUpdate.Email);

                    }
                    else if (volunteerToUpdate.Active == true && ActiveStatus == false)
                    {
                        //You reactivating the user, create them and
                        //give them the selected roles
                        InsertIdentityUser(volunteerToUpdate.Email, selectedRoles);
                    }
                    else if (volunteerToUpdate.Active == true && ActiveStatus == true)
                    {
                        //No change to Active status so check for a change in Email
                        //If you Changed the email, Delete the old login and create a new one
                        //with the selected roles
                        if (volunteerToUpdate.Email != databaseEmail)
                        {
                            //Add the new login with the selected roles
                            InsertIdentityUser(volunteerToUpdate.Email, selectedRoles);

                            //This deletes the user's old login from the security system
                            await DeleteIdentityUser(databaseEmail);
                        }
                        else
                        {
                            //Finially, Still Active and no change to Email so just Update
                            await UpdateUserRoles(selectedRoles, volunteerToUpdate.Email);
                        }
                    }
                    TempData["Message"] = "You have successfully updated a Account.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteerToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                    {
                        ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email addresses.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            //We are here because something went wrong and need to redisplay
            VolunteerAdminVM volunterAdminVM = new VolunteerAdminVM
            {
                Phone = volunteerToUpdate.Phone,
                Email = volunteerToUpdate.Email,
                Active = volunteerToUpdate.Active,
                ID = volunteerToUpdate.ID,
                FirstName = volunteerToUpdate.FirstName,
                LastName = volunteerToUpdate.LastName,
            };
            foreach (var role in selectedRoles)
            {
                volunterAdminVM.UserRoles.Add(role);
            }
            PopulateAssignedRoleData(volunterAdminVM);
            return View(volunterAdminVM);
        }

        private void PopulateAssignedRoleData(VolunteerAdminVM volunteer)
        {//Prepare checkboxes for all Roles
            var allRoles = _identityContext.Roles;
            var currentRoles = volunteer.UserRoles;
            var viewModel = new List<RoleVM>();
            foreach (var r in allRoles)
            {
                viewModel.Add(new RoleVM
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    Assigned = currentRoles.Contains(r.Name)
                });
            }
            ViewBag.Roles = viewModel;
        }

        private async System.Threading.Tasks.Task UpdateUserRoles(string[] selectedRoles, string Email)
        {
            var _user = await _userManager.FindByEmailAsync(Email);//IdentityUser
            if (_user != null)
            {
                var UserRoles = (List<string>)await _userManager.GetRolesAsync(_user);//Current roles user is in

                if (selectedRoles == null)
                {
                    //No roles selected so just remove any currently assigned
                    foreach (var r in UserRoles)
                    {
                        await _userManager.RemoveFromRoleAsync(_user, r);
                    }
                }
                else
                {
                    //At least one role checked so loop through all the roles
                    //and add or remove as required

                    //We need to do this next line because foreach loops don't always work well
                    //for data returned by EF when working async.  Pulling it into an IList<>
                    //first means we can safely loop over the colleciton making async calls and avoid
                    //the error 'New transaction is not allowed because there are other threads running in the session'
                    IList<IdentityRole> allRoles = _identityContext.Roles.ToList<IdentityRole>();

                    foreach (var r in allRoles)
                    {
                        if (selectedRoles.Contains(r.Name))
                        {
                            if (!UserRoles.Contains(r.Name))
                            {
                                await _userManager.AddToRoleAsync(_user, r.Name);
                            }
                        }
                        else
                        {
                            if (UserRoles.Contains(r.Name))
                            {
                                await _userManager.RemoveFromRoleAsync(_user, r.Name);
                            }
                        }
                    }
                }
            }
        }

        private void InsertIdentityUser(string Email, string[] selectedRoles)
        {
            //Create the IdentityUser in the IdentitySystem
            //Note: this is similar to what we did in ApplicationSeedData
            if (_userManager.FindByEmailAsync(Email).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = Email,
                    Email = Email,
                    EmailConfirmed = true //since we are creating it!
                };
                //Create a random password with a default 8 characters
                string password = "Pa$$w@rd"; //MakePassword.Generate();
                IdentityResult result = _userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    foreach (string role in selectedRoles)
                    {
                        _userManager.AddToRoleAsync(user, role).Wait();
                    }
                }
            }
            else
            {
                TempData["message"] = "The Login Account for " + Email + " was already in the system.";
            }
        }

        private async System.Threading.Tasks.Task DeleteIdentityUser(string Email)
        {
            var userToDelete = await _identityContext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            if (userToDelete != null)
            {
                _identityContext.Users.Remove(userToDelete);
                await _identityContext.SaveChangesAsync();
            }
        }

        private async System.Threading.Tasks.Task InviteUserToResetPassword(Account volunteer, string message)
        {
            message ??= "<p>Hello " + volunteer.FirstName + "</p><p>To complete your account registration, please visit " +
                        "<a href='https://niagaracollegepacproject2023.azurewebsites.net/' title='https://niagaracollegepacproject2023.azurewebsites.net/' target='_blank' rel='noopener'>" +
                        "Program Advisory Committees</a>" + " and create a new password for " + volunteer.Email + " using Forgot Password to access the site.<br /><br />"
                        + "Sincerly,<br />"+ "<b>Program Advisory Committees Team</b></p>";
            try
            {
                await _emailSender.SendOneAsync(volunteer.FullName, volunteer.Email,
                "Niagara College Program Advisory Committees| Account Registration ", message);
                TempData["message"] = "Invitation email sent to " + volunteer.FullName + " at " + volunteer.Email;
            }
            catch (Exception)
            {
                TempData["message"] = "Could not send Invitation email to " + volunteer.FullName + " at " + volunteer.Email;
            }


        }

        private bool VolunteerExists(int id)
        {
          return _context.Accounts.Any(e => e.ID == id);
        }

        //private IdentityUser CreateUser()
        //{
        //    try
        //    {
        //        return Activator.CreateInstance<IdentityUser>();
        //    }
        //    catch
        //    {
        //        throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
        //            $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
        //            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        //    }
        //}
    }
}
