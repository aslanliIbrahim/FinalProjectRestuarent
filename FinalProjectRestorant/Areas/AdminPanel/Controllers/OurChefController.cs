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
    public class OurChefController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public OurChefController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.ourChefs);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OurChef ourChef)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!ourChef.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (ourChef.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + ourChef.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "our-chef-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await ourChef.Photo.CopyToAsync(fileStream);
            }
            ourChef.Image = fileName;
            _context.ourChefs.Add(ourChef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //update side start
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var chef = await _context.ourChefs.FirstOrDefaultAsync(ch=>ch.Id==id);
            if (chef == null)
                return NotFound();
            return View(chef);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, OurChef ourChef)
        {
            
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (ourChef == null)
                return NotFound();

            //remove img
            string enviroment = _env.WebRootPath;
            //string folderpath = Path.Combine(enviroment, "photos", "Our-chef-photo", ourChef.Image);
            //FileInfo oldfile = new FileInfo(folderpath);
            //if (System.IO.File.Exists(folderpath))
            //{
            //    oldfile.Delete();
            //};
            //remove end
            //update img
            string filename = Guid.NewGuid().ToString() + '-' + ourChef.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Our-chef-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                ourChef.Photo.CopyTo(newFile);
            }
            var ourChefs = await _context.ourChefs.FirstOrDefaultAsync(ch => ch.Id == id);
            ourChefs.Image = filename;
            ourChefs.Name = ourChef.Name;
            ourChefs.Position = ourChef.Position;
            ourChefs.Face = ourChef.Face;
            ourChefs.Insta = ourChef.Insta;
            ourChefs.Twit = ourChef.Twit;
            ourChefs.AboutChef = ourChef.AboutChef;
            //_context.ourChefs.Update(ourChef);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            //update end
        }

       
        //delete side
        public IActionResult Delete(int? id)
        {
            var chef = _context.ourChefs.FirstOrDefault(ch=>ch.Id==id);
            _context.ourChefs.Remove(chef);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
