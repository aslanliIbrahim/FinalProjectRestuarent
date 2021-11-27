using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ContactVM contactVm = new ContactVM
            {
                HowToReach = _context.Contacts.FirstOrDefault().HowToReach,
                Adress = _context.Settings.FirstOrDefault().Adress,
                PhoneNumber = _context.Settings.FirstOrDefault().PhoneNumber,
                SocialMedia = _context.Settings.FirstOrDefault().SocialMedia,
                Email = _context.Settings.FirstOrDefault().Email
            };
            return View(contactVm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(ContactVM contactVM)
        {
            if (!ModelState.IsValid) return View(contactVM);

            Sms contact = new Sms
            {
                Name = contactVM.Name,
                Email = contactVM.Email,
                Message = contactVM.Message
            };
            
            
            await _context.Sms.AddAsync(contact);
            await _context.SaveChangesAsync();


            var customer = new SmtpClient();
            customer.Host = "smtp.gmail.com";
            customer.EnableSsl = true;
            customer.Port = 587;
            customer.Credentials = new System.Net.NetworkCredential("ibrahimra@code.edu.az", "Y7GDj5BR");

            var mailMessage = new System.Net.Mail.MailMessage("ibrahimra@code.edu.az", contact.Email);

            mailMessage.Subject = "Mailininz qebul olundu";
            mailMessage.Body = contact.Message;
            mailMessage.Priority = MailPriority.High;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            //mailMessage.IsBodyHtml = contact.Message;
 
            customer.Send(mailMessage);

            customer.Dispose();

            return RedirectToAction(nameof(Index));
        }

    }
}
