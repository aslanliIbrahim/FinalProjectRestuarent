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
    public class BreakFastController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public BreakFastController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.breakFasts);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BreakFast breakFast)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!breakFast.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (breakFast.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + breakFast.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await breakFast.Photo.CopyToAsync(fileStream);
            }
            breakFast.Image = fileName;
            _context.breakFasts.Add(breakFast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //update is start
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var breakFast = await _context.breakFasts.FirstOrDefaultAsync(bt => bt.Id == id);
            if (breakFast == null)
                return NotFound();
            return View(breakFast);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, BreakFast breakFast)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (breakFast == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + breakFast.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Menu-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                breakFast.Photo.CopyTo(newFile);
            }

            //new img end   
            var breakfastDb = await _context.breakFasts.FirstOrDefaultAsync(bt => bt.Id == id);
            breakfastDb.Image = filename;
            breakfastDb.BigMenuFoodName = breakFast.BigMenuFoodName;
            breakfastDb.Ingredient = breakFast.Ingredient;
            breakfastDb.Pieces = breakFast.Pieces;
            breakfastDb.Price = breakFast.Price;
            breakfastDb.Servis = breakFast.Servis;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //update is end

        //delete is start
        public IActionResult Delete(int? id)
        {
            var menus = _context.breakFasts.FirstOrDefault(bt => bt.Id == id);
            _context.breakFasts.Remove(menus);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //delete is end
    }
}
