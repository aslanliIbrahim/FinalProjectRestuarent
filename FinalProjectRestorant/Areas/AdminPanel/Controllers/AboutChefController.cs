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
    public class AboutChefController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public AboutChefController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.AboutChefHomes);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutChefHome aboutChefHome)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!aboutChefHome.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (aboutChefHome.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + aboutChefHome.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Home-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await aboutChefHome.Photo.CopyToAsync(fileStream);
            }
            aboutChefHome.Image = fileName;
            _context.AboutChefHomes.Add(aboutChefHome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var aboutChef = await _context.AboutChefHomes.FirstOrDefaultAsync(st => st.Id == id);
            if (aboutChef == null)
                return NotFound();
            return View(aboutChef);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, AboutChefHome aboutChefHome)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (aboutChefHome == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + aboutChefHome.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Home-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                aboutChefHome.Photo.CopyTo(newFile);
            }

            //new img end 
            var aboutchefDb = await _context.AboutChefHomes.FirstOrDefaultAsync(st => st.Id == id);
            aboutchefDb.Image = filename;
            aboutchefDb.ChefAbout = aboutChefHome.ChefAbout;
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //delete is start here..
        public IActionResult Delete(int? id)
        {
            var aboutchef = _context.AboutChefHomes.FirstOrDefault(st => st.Id == id);
            _context.AboutChefHomes.Remove(aboutchef);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //end here..
    }
}
