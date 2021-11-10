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
    public class SlidesController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public SlidesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Slides);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slides slide)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!slide.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (slide.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + slide.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos","our-chef-photo", fileName);
            using(FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await slide.Photo.CopyToAsync(fileStream);
            }
            slide.Image = fileName;
            _context.Slides.Add(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //update is start here
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var slide = await _context.Slides.FirstOrDefaultAsync(sl => sl.Id == id);
            if (slide == null)
                return NotFound();
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slides slides)
        {

            if (!ModelState.IsValid) 
                return View();
            if (id == null)
                return NotFound();
            
            if (slides == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + slides.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Our-chef-photo", filename);
            using(FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                slides.Photo.CopyTo(newFile);
            }

            //new img end 
            var slidesDb = await _context.Slides.FirstOrDefaultAsync(sl=>sl.Id == id);
            slidesDb.Image = filename;
            slidesDb.RestaurantName = slides.RestaurantName;
            slidesDb.Saw = slides.Saw;
            slidesDb.Description = slides.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //update is end

        public IActionResult Delete(int? id)
        {
            var slide = _context.Slides.FirstOrDefault(sl => sl.Id == id);
            _context.Slides.Remove(slide);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
