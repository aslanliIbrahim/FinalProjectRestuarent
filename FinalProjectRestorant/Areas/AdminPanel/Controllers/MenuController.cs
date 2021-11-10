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




    }
}
