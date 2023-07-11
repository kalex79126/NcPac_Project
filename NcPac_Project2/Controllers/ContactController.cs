using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NcPac_Project2.Utilities;
using NcPac_Project2.ViewModels;

namespace NcPac_Project2.Controllers
{
    public class ContactController : Controller
    {
        private readonly NC_PAC_Context _context;
        private readonly IMyEmailSender _emailSender;

        public ContactController(NC_PAC_Context context, IMyEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContactForm(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                return View("Index", model);

            }

            var emailMessage = new EmailMessage
            {
                ToAddresses = new List<EmailAddress>
                {
                    new EmailAddress { Address = "NcPacInvoice@outlook.com" } //address where we want to recieve emails
                },
                Subject = model.Subject,
                Content = $"<p><strong>Name:</strong> {model.Name}</p><p><strong>Email:</strong> {model.Email}</p><p><strong>Message:</strong> {model.Message}</p>"
            };

            await _emailSender.SendToManyAsync(emailMessage);
            ViewBag.Message = "Thank you for your message. We will be in touch soon!";
            return View(model);
        }

    }
}