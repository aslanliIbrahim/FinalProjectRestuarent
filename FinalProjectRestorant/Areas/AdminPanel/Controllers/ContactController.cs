using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ContactController : Controller
    {
        public AppDbContext _context { get; }
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Contacts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid) return View();
            bool hasContact = _context.Contacts.Any(c => c.HowToReach == contact.HowToReach);
            if (hasContact)
            {
                ModelState.AddModelError("HowtoReach","This text heas already exist");
                return View();
            }
             _context.Contacts.Add(contact);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var howTo = await _context.Contacts.FirstOrDefaultAsync(c =>c.Id == id);
            if (howTo == null)
                return NotFound();
            return View(howTo);
        }
        [HttpPost]
        public IActionResult Update(int? id,Contact contact)
        {
            if (id == null)
                return NotFound();
           
            var howTo =  _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (howTo == null)
                return NotFound();
            bool hasContact = _context.Contacts.Any(c => c.HowToReach == contact.HowToReach);
            if (hasContact)
            {
                ModelState.AddModelError("HowtoReach", "This text heas already exist");
                return View(howTo);
            }
            howTo.HowToReach = contact.HowToReach;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var HowTo = _context.Contacts.FirstOrDefault(cn => cn.Id == id);
            _context.Contacts.Remove(HowTo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
