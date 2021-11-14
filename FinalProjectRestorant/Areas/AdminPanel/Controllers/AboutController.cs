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
    public class AboutController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Abouts);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!about.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (about.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + about.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "about-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await about.Photo.CopyToAsync(fileStream);
            }
            about.Image = fileName;
            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //update is start
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var abouts = await _context.Abouts.FirstOrDefaultAsync(ab => ab.Id == id);
            if (abouts == null)
                return NotFound();
            return View(abouts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, About about)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (about == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + about.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "about-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                about.Photo.CopyTo(newFile);
            }

            //new img end   
            var AboutDB = await _context.Abouts.FirstOrDefaultAsync(bt => bt.Id == id);
            AboutDB.Image = filename;
            AboutDB.Title = about.Title;
            AboutDB.Description = about.Description;
            AboutDB.GourMet = about.GourMet;
            AboutDB.CleanTastes = about.CleanTastes;
            AboutDB.ModernEnvironment = about.ModernEnvironment;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //update is end



        //delete is start
        public IActionResult Delete(int? id)
        {
            var about = _context.Abouts.FirstOrDefault(at => at.Id == id);
            _context.Abouts.Remove(about);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //delete is end
    }
}
