using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Controllers
{
    public class BreakFastController : Controller
    {
        private readonly AppDbContext _context;
        public BreakFastController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            decimal pageItemCount = 3;
            decimal b = Math.Ceiling(_context.breakFasts.Count() / pageItemCount);
            ViewBag.pageCount = Convert.ToInt32(b);
            ViewBag.ActivePage = page;


            List<BreakFast> breakFasts = await _context.breakFasts.OrderBy(st => st.Id)
                                                                           .Skip((page - 1) * (int)pageItemCount)
                                                                           .Take((int)pageItemCount)
                                                                           .ToListAsync();



            BreakFastVM breakFastVM = new BreakFastVM
            {
                slides = _context.Slides.ToList(),
                breakFasts = breakFasts
            };
            return View(breakFastVM);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            var slides = await _context.Slides.ToListAsync();
            ViewBag.Slides = slides;

            var order = await _context.breakFasts.FirstOrDefaultAsync(p => p.Id == id);
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
        public async Task<IActionResult> Detail(BreakFast breakFast, int? id, string allPrice, string count)
        {
            if (id is null)
                return BadRequest();

            var slides = await _context.Slides.ToListAsync();
            ViewBag.Slides = slides;

            var dbBreakFast = await _context.breakFasts.FirstOrDefaultAsync(p => p.Id == id);
            if (dbBreakFast == null)
                return NotFound();

            AdminOrder adminOrder = new AdminOrder
            {
                Image = dbBreakFast.Image,
                NameOfFood = dbBreakFast.BigMenuFoodName,
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
