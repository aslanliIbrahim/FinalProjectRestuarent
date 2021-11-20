using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Controllers
{
    public class DrinksController : Controller
    {

        private readonly AppDbContext _context;
        public DrinksController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            decimal pageItemCount = 3;
            decimal b = Math.Ceiling(_context.drinks.Count() / pageItemCount);
            ViewBag.pageCount = Convert.ToInt32(b);
            ViewBag.ActivePage = page;


            List<Drinks> Drinks = await _context.drinks.OrderBy(dr => dr.Id)
                                                                           .Skip((page - 1) * (int)pageItemCount)
                                                                           .Take((int)pageItemCount)
                                                                           .ToListAsync();

            DrinkVm drink = new DrinkVm
            {
                slides = _context.Slides.ToList(),
                drinks = Drinks
            };
            return View(drink);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            var slides = await _context.Slides.ToListAsync();
            ViewBag.Slides = slides;

            var order = await _context.drinks.FirstOrDefaultAsync(p => p.Id == id);
            if (order == null)
                return NotFound();

            //OrderVM orders = new OrderVM
            //{
            //    Slides = _context.Slides.ToList(),
            //    Order = await _context.SteakBigMenus.FirstOrDefaultAsync(p => p.Id == id),

            //};
            //if (orders.Order == null)
            //    return NotFound();


            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(Drinks drinks, int? id, string allPrice, string count)
        {
            if (id is null)
                return BadRequest();

            var slides = await _context.Slides.ToListAsync();
            ViewBag.Slides = slides;

            var dbDrinks = await _context.drinks.FirstOrDefaultAsync(p => p.Id == id);
            if (dbDrinks == null)
                return NotFound();

            AdminOrder adminOrder = new AdminOrder
            {
                Image = dbDrinks.Image,
                NameOfFood = dbDrinks.BigMenuFoodName,
                Price = Convert.ToDouble(allPrice),
                Count = Convert.ToInt32(count)
            };
            _context.AdminOrders.Add(adminOrder);
            await _context.SaveChangesAsync();
            //Home sehifeye qayidacag burani fix edersen sonra;
            return RedirectToAction("Index", "Home");
        }
    }
}
