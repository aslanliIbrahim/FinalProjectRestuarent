using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class DrinksController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public DrinksController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.drinks);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Drinks drinks)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!drinks.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (drinks.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + drinks.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await drinks.Photo.CopyToAsync(fileStream);
            }
            drinks.Image = fileName;
            _context.drinks.Add(drinks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //delete is start
        public IActionResult Delete(int? id)
        {
            var drinks = _context.drinks.FirstOrDefault(dt => dt.Id == id);
            _context.drinks.Remove(drinks);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //delete is end
    }
}
