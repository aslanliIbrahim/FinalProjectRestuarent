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
    public class SettingController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Settings);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setting setting)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!setting.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (setting.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + setting.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await setting.Photo.CopyToAsync(fileStream);
            }
            setting.Image = fileName;
            _context.Settings.Add(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //update start here..
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var setting = await _context.Settings.FirstOrDefaultAsync(st => st.Id == id);
            if (setting == null)
                return NotFound();
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Setting setting)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (setting == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + setting.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "navbar-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                setting.Photo.CopyTo(newFile);
            }

            //new img end 
            var SettingDb = await _context.Settings.FirstOrDefaultAsync(st => st.Id == id);
            SettingDb.Image = filename;
            SettingDb.Adress = setting.Adress;
            SettingDb.Eogo = setting.Eogo;
            SettingDb.Email = setting.Email;
            SettingDb.PhoneNumber = setting.PhoneNumber;
            SettingDb.SocialMedia = setting.SocialMedia;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //update end here..
        //delete is start here..
        public IActionResult Delete(int? id)
        {
            var setting = _context.Settings.FirstOrDefault(st => st.Id == id);
            _context.Settings.Remove(setting);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //end here..
    }
}
