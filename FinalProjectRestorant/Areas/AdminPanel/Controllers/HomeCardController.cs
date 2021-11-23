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
    public class HomeCardController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public HomeCardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.HomeCards);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeCard homeCard)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!homeCard.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (homeCard.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + homeCard.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Home-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await homeCard.Photo.CopyToAsync(fileStream);
            }
            homeCard.Image = fileName;
            _context.HomeCards.Add(homeCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //update start here..
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var homecard= await _context.HomeCards.FirstOrDefaultAsync(hm => hm.Id == id);
            if (homecard == null)
                return NotFound();
            return View(homecard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, HomeCard homeCard)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (homeCard == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + homeCard.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Home-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                homeCard.Photo.CopyTo(newFile);
            }

            //new img end 
            var homecardDb = await _context.HomeCards.FirstOrDefaultAsync(hm => hm.Id == id);
            homecardDb.Image = filename;
            homecardDb.Title = homeCard.Title;
            homecardDb.Description = homeCard.Description;
            homecardDb.TypeOfFood = homeCard.TypeOfFood;
            homecardDb.AspController = homeCard.AspController;
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //update end here..


        //delete is start here..
        public IActionResult Delete(int? id)
        {
            var menus = _context.HomeCards.FirstOrDefault(hm => hm.Id == id);
            _context.HomeCards.Remove(menus);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //end here..
    }
}
