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

    public class MenuController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public MenuController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Menus);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!menu.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (menu.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + menu.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await menu.Photo.CopyToAsync(fileStream);
            }
            menu.Image = fileName;
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //update is here.
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var menus = await _context.Menus.FirstOrDefaultAsync(mn => mn.Id == id);
            if (menus == null)
                return NotFound();
            return View(menus);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Menu menu)
        {

            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (menu == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + menu.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Menu-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                menu.Photo.CopyTo(newFile);
            }

            //new img end 
            var menuDb = await _context.Menus.FirstOrDefaultAsync(mn => mn.Id == id);
            menuDb.Image = filename;
            menuDb.FoodName = menu.FoodName;            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //updatte is end here.

        //delete is here.
        public IActionResult Delete(int? id)
        {
            var menus = _context.Menus.FirstOrDefault(mn => mn.Id == id);
            _context.Menus.Remove(menus);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
