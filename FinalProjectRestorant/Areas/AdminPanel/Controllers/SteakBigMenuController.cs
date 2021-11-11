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
    public class SteakBigMenuController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public SteakBigMenuController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.SteakBigMenus);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SteakBigMenu steakBigMenu)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!steakBigMenu.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (steakBigMenu.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + steakBigMenu.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await steakBigMenu.Photo.CopyToAsync(fileStream);
            }
            steakBigMenu.Image = fileName;
            _context.SteakBigMenus.Add(steakBigMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //update start here..
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var steak = await _context.SteakBigMenus.FirstOrDefaultAsync(st => st.Id == id);
            if (steak == null)
                return NotFound();
            return View(steak);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SteakBigMenu steakBigMenu)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (steakBigMenu == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + steakBigMenu.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Menu-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                steakBigMenu.Photo.CopyTo(newFile);
            }

            //new img end 
            var steakmenuDb = await _context.SteakBigMenus.FirstOrDefaultAsync(st => st.Id == id);
            steakmenuDb.Image = filename;
            steakmenuDb.BigMenuFoodName = steakBigMenu.BigMenuFoodName;
            steakmenuDb.Ingredient = steakBigMenu.Ingredient;
            steakmenuDb.Pieces = steakBigMenu.Pieces;
            steakmenuDb.Price = steakBigMenu.Price;
            steakmenuDb.Servis = steakBigMenu.Servis;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //update end here..
        //delete is start here..
        public IActionResult Delete(int? id)
        {
            var menus = _context.SteakBigMenus.FirstOrDefault(st => st.Id == id);
            _context.SteakBigMenus.Remove(menus);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //end here..
    }
}
