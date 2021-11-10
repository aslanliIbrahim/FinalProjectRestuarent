using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return RedirectToAction(nameof(Index));
        }

    }
}
