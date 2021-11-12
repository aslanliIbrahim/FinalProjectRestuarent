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
    public class PizzaMenuController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public PizzaMenuController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Pizzas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pizza pizza)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!pizza.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please enter image format");
                return View();
            }
            if (pizza.Photo.Length / 1024 > 500)
            {
                ModelState.AddModelError("Photo", "image size must be less 500kb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + pizza.Photo.FileName;
            string resulPath = Path.Combine(_env.WebRootPath, "photos", "Menu-photo", fileName);
            using (FileStream fileStream = new FileStream(resulPath, FileMode.Create))
            {
                await pizza.Photo.CopyToAsync(fileStream);
            }
            pizza.Image = fileName;
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //update is start
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(pi => pi.Id == id);
            if (pizza == null)
                return NotFound();
            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Pizza pizzas)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();

            if (pizzas == null)
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
            string filename = Guid.NewGuid().ToString() + '-' + pizzas.Photo.FileName;
            string newSlider = Path.Combine(enviroment, "photos", "Menu-photo", filename);
            using (FileStream newFile = new FileStream(newSlider, FileMode.Create))
            {
                pizzas.Photo.CopyTo(newFile);
            }

            //new img end   
            var PizzaDB = await _context.Pizzas.FirstOrDefaultAsync(bt => bt.Id == id);
            PizzaDB.Image = filename;
            PizzaDB.BigMenuFoodName = pizzas.BigMenuFoodName;
            PizzaDB.Ingredient = pizzas.Ingredient;
            PizzaDB.Pieces = pizzas.Pieces;
            PizzaDB.Price = pizzas.Price;
            PizzaDB.Servis = pizzas.Servis;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //update is end
        //delete is start
        public IActionResult Delete(int? id)
        {
            var menus = _context.Pizzas.FirstOrDefault(pi => pi.Id == id);
            _context.Pizzas.Remove(menus);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //delet end
    }
}
