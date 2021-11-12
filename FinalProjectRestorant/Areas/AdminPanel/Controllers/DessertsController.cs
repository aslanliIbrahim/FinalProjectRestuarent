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
    public class DessertsController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public DessertsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.desserts);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Desserts desserts)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!desserts.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (desserts.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + desserts.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await desserts.Photo.CopyToAsync(fileStream);
            }
            desserts.Image = fileName;
            _context.desserts.Add(desserts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //update is start
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var desserts = await _context.desserts.FirstOrDefaultAsync(bt => bt.Id == id);
            if (desserts == null)
                return NotFound();
            return View(desserts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Desserts desserts)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (desserts == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + desserts.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Menu-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                desserts.Photo.CopyTo(newFile);
            }

            //new img end   
            var dessertsDB = await _context.desserts.FirstOrDefaultAsync(st => st.Id == id);
            dessertsDB.Image = filename;
            dessertsDB.BigMenuFoodName = desserts.BigMenuFoodName;
            dessertsDB.Ingredient = desserts.Ingredient;
            dessertsDB.Pieces = desserts.Pieces;
            dessertsDB.Price = desserts.Price;
            dessertsDB.Servis = desserts.Servis;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //update is end
        //delete is start
        public IActionResult Delete(int? id)
        {
            var desserts = _context.desserts.FirstOrDefault(dt => dt.Id == id);
            _context.desserts.Remove(desserts);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //delete is end
    }
}
