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
    public class StartersController : Controller
    {
        private readonly AppDbContext _context;
        public StartersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            decimal pageItemCount = 3;
            decimal b = Math.Ceiling(_context.starters.Count() / pageItemCount);
            ViewBag.pageCount = Convert.ToInt32(b);
            ViewBag.ActivePage = page;


            List<Starters> starters = await _context.starters.OrderBy(st => st.Id)
                                                                           .Skip((page - 1) * (int)pageItemCount)
                                                                           .Take((int)pageItemCount)
                                                                           .ToListAsync();

            StarTersVM starTers = new StarTersVM
            {
                slides = _context.Slides.ToList(),
                Starters = starters
            };
            return View(starTers);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            var slides = await _context.Slides.ToListAsync();
            ViewBag.Slides = slides;

            var order = await _context.starters.FirstOrDefaultAsync(p => p.Id == id);
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
        public async Task<IActionResult> Detail(Starters starters, int? id, string allPrice, string count)
        {
            if (id is null)
                return BadRequest();

            var slides = await _context.Slides.ToListAsync();
            ViewBag.Slides = slides;

            var dbStarters = await _context.starters.FirstOrDefaultAsync(p => p.Id == id);
            if (dbStarters == null)
                return NotFound();

            AdminOrder adminOrder = new AdminOrder
            {
                Image = dbStarters.Image,
                NameOfFood = dbStarters.BigMenuFoodName,
                Price = Convert.ToDouble(allPrice),
                Count = Convert.ToInt32(count)
            };
            _context.AdminOrders.Add(adminOrder);
            await _context.SaveChangesAsync();
            //Home sehifeye qayidacag burani fix edersen sonra;
            return RedirectToAction("Index", "Menu");
        }
    }
}
