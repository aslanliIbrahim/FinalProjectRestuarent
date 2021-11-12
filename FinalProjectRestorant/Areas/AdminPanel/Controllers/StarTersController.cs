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
    public class StarTersController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public StarTersController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.starters);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Starters starters)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!starters.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (starters.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + starters.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await starters.Photo.CopyToAsync(fileStream);
            }
            starters.Image = fileName;
            _context.starters.Add(starters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //update is start
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var breakFast = await _context.starters.FirstOrDefaultAsync(st => st.Id == id);
            if (breakFast == null)
                return NotFound();
            return View(breakFast);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Starters starters)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (starters == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + starters.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Menu-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                starters.Photo.CopyTo(newFile);
            }

            //new img end   
            var startersDB = await _context.starters.FirstOrDefaultAsync(st => st.Id == id);
            startersDB.Image = filename;
            startersDB.BigMenuFoodName = starters.BigMenuFoodName;
            startersDB.Ingredient = starters.Ingredient;
            startersDB.Pieces = starters.Pieces;
            startersDB.Price = starters.Price;
            startersDB.Servis = starters.Servis;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //update is end
        //delete is start
        public IActionResult Delete(int? id)
        {
            var starters = _context.starters.FirstOrDefault(st => st.Id == id);
            _context.starters.Remove(starters);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //delete is end
    }
}
