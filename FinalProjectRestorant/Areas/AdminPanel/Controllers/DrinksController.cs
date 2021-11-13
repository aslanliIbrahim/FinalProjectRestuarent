using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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

        //update is start
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var drinks = await _context.drinks.FirstOrDefaultAsync(dt => dt.Id == id);
            if (drinks == null)
                return NotFound();
            return View(drinks);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Drinks drinks)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (drinks == null)
                return NotFound();

            //remove old img
            string enviroment = _env.WebRootPath;
            //string folderpath = Path.Combine(enviroment, "photos", "Our-chef-photo", slides.Photo.FileName) ;
            //FileInfo oldfile = new FileInfo(folderpath);
            //if (System.IO.File.Exists(folderpath))
            //{
            //    oldfile.Delete();
            //};
            //remove end

            //new img in local folder
            string filename = Guid.NewGuid().ToString() + '-' + drinks.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Menu-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                drinks.Photo.CopyTo(newFile);
            }

            //new img end   
            var DrinksDB = await _context.drinks.FirstOrDefaultAsync(dt => dt.Id == id);
            DrinksDB.Image = filename;
            DrinksDB.BigMenuFoodName = drinks.BigMenuFoodName;
            DrinksDB.Ingredient = drinks.Ingredient;
            DrinksDB.Pieces = drinks.Pieces;
            DrinksDB.Price = drinks.Price;
            DrinksDB.Servis = drinks.Servis;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //update is end

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
