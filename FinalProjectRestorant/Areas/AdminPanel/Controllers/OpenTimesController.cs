using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class OpenTimesController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public OpenTimesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.OpenTimes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OpenTimes openTimes)
        {
            if (!ModelState.IsValid)
                return View();
            bool hasOpenTimes = _context.OpenTimes.Any(c => c.Hours.ToLower() == openTimes.Hours.ToLower());
            bool HasOpenTimes = _context.OpenTimes.Any(c => c.DinnerHours.ToLower() == openTimes.DinnerHours.ToLower());
            bool HasDay = _context.OpenTimes.Any(c => c.Day.ToLower() == openTimes.Day.ToLower());
            bool MealTime = _context.OpenTimes.Any(c => c.MealTime.ToLower() == openTimes.MealTime.ToLower());
            //if (hasOpenTimes || HasOpenTimes || HasDay )
            //{
            //    ModelState.AddModelError("Dinner or launch times", "This text heas already exist");
            //    return View();
            //}
            _context.OpenTimes.Add(openTimes);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var opentimes = await _context.OpenTimes.FirstOrDefaultAsync(c => c.Id == id);
            if (opentimes == null)
                return NotFound();
            return View(opentimes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int? id, OpenTimes opentimes)
        {
            if (id == null)
                return NotFound();

            var opentimesdb = _context.OpenTimes.FirstOrDefault(c => c.Id == id);
            if (opentimes == null)
                return NotFound();
            bool hasOpenTimes = _context.OpenTimes.Any(c => c.Hours.ToLower() == opentimes.Hours.ToLower());
            bool HasOpenTimes = _context.OpenTimes.Any(c => c.DinnerHours.ToLower() == opentimes.DinnerHours.ToLower());
            bool HasDay = _context.OpenTimes.Any(c => c.Day.ToLower() == opentimes.Day.ToLower());
            bool MealTime = _context.OpenTimes.Any(c => c.MealTime.ToLower() == opentimes.MealTime.ToLower());
            //if (hasContact)
            //{
            //    ModelState.AddModelError("HowtoReach", "This text heas already exist");
            //    return View(opentimes);
            //}
            opentimes.Hours = opentimesdb.Hours;
            opentimes.DinnerHours = opentimesdb.DinnerHours;
            opentimes.Day = opentimesdb.Day;
            opentimes.MealTime = opentimesdb.MealTime;
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var openTimes = _context.OpenTimes.FirstOrDefault(cn => cn.Id == id);
            _context.OpenTimes.Remove(openTimes);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
