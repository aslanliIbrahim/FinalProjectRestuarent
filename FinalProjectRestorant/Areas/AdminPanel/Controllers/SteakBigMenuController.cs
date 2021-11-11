using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    }
}
