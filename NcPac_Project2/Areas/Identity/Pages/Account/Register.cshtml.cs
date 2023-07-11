// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NcPac_Project2.Models.Enums;

namespace NcPac_Project2.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Personal Information data validations fields, for Register form
            /// </summary>
            /// 

            //Committees
            [Required(ErrorMessage = "You must select a Committee")]
            [Display(Name = "Committees")]
            public Committees Committees { get; set; }


            //Name
            [Required]
            [StringLength(50, ErrorMessage = "The Name field must be at max 50 characters long")]
            [Display(Name ="Name")]
            public string Name { get; set; }

            //Street Address
            [Required]
            [StringLength(50, ErrorMessage = "The Street field Address must be at max 70 characters long")]
            [Display(Name = "Street Address")]
            public string StreetAddress { get; set; }

            //City
            [Required]
            [StringLength(50, ErrorMessage = "The City field must be at max 50 characters long")]
            [Display(Name = "City")]
            public string City { get; set; }

            //Province
            [Required(ErrorMessage ="You must select a Province")]
            [Display(Name = "Province")]
            public Provinces? Province { get; set; }

            //Postal Code
            [Required]
            [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)",
                ErrorMessage ="The Postal Code format is incorrect")]
            [StringLength(7, ErrorMessage = "The Postal Code field must be at max 6 characters long or 7 if it includes a blank space")]
            [Display(Name = "Postal Code")]
            public string PostalCode { get; set; }

           
            //Personal Telephone
            [Required]
            [PhoneAttribute]
            [RegularExpression(@"^(\d{10})$", ErrorMessage = "The Phone Number format is incorrect")]
            [StringLength(10, ErrorMessage ="The Telephone number field must be 10 digits characters long",MinimumLength =10)]
            [DataType(DataType.PhoneNumber)]
            [Display(Name ="Telephone Number")]
            public string TelephoneNumber { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }



            /// <summary>
            /// Employment Information data validations fields, for Register form
            /// </summary>

            //Position Title
            [Required]
            [StringLength(50, ErrorMessage = "The Position Title field must be at max 50 digits characters long")]
            [Display(Name = "Position Title")]
            public string PositionTitle { get; set; }

            //Company Name
            [Required]
            [StringLength(70, ErrorMessage = "The Company Name field must be at max 70 digits characters long")]
            [Display(Name = "Company Name")]
            public string CompanyName { get; set; }

            //Company Street Address
            [Required]
            [StringLength(50, ErrorMessage = "The Company Street field Address must be at max 70 characters long")]
            [Display(Name = " Company Street Address")]
            public string CompanyStreetAddress { get; set; }

            //Company City
            [Required]
            [StringLength(50, ErrorMessage = "The Company City field must be at max 50 characters long")]
            [Display(Name = "Company City")]
            public string CompanyCity { get; set; }

            //Company Province
            [Required(ErrorMessage = "You must select a Province")]
            [Display(Name = "Company Province")]
            public Provinces CompanyProvince { get; set; }


            //Company Postal Code
            [Required]
            [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)",
                ErrorMessage = "The Postal Code format is incorrect")]
            [StringLength(7, ErrorMessage = "The Company Postal Code field must be at max 6 characters long or 7 if it includes a blank space")]
            [Display(Name = "Company Postal Code")]
            public string CompanyPostalCode { get; set; }

            //Company Telephone
            [Required]
            [PhoneAttribute]
            [RegularExpression(@"^(\d{10})$", ErrorMessage = "The Phone Number format is incorrect")]
            [StringLength(10, ErrorMessage = "The Telephone number field must be 10 digits characters long", MinimumLength = 10)]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Company Telephone Number")]
            public string CompanyTelephoneNumber { get; set; }

            //Company Email
            [Required]
            [EmailAddress]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Company Email")]
            public string CompanyEmail { get; set; }


        }


        public async System.Threading.Tasks.Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                
                //take this out if you want to actually confirm email accounts
                user.EmailConfirmed = true;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
