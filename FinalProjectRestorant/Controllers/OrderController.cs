using FinalProjectRestorant.DAL;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            OrderVM orderVM = new OrderVM
            {
                Slides =await _context.Slides.ToListAsync(),
                SteakBigMenus =await _context.SteakBigMenus.ToListAsync(),
                BreakFasts = await _context.breakFasts.ToListAsync(),
                Pizzas = await _context.Pizzas.ToListAsync(),
                Starters = await _context.starters.ToListAsync(),
                Desserts = await _context.desserts.ToListAsync(),
                Drinks = await _context.drinks.ToListAsync(),
            };
            return View(orderVM);
        }
        public async Task<IActionResult> SteakDetail(int? id)
        {
            OrderVM orders = new OrderVM
            {
                Order =await _context.SteakBigMenus.FirstOrDefaultAsync(p => p.Id == id),

            };
            //if (orders.Order == null)
            //    return ();
           
          
            
            return View(orders);
        }


    }
}
