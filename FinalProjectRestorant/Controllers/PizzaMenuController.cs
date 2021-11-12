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
    public class PizzaMenuController : Controller
    {
        private readonly AppDbContext _context;
        public PizzaMenuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            PizzaVM pizza = new PizzaVM
            {
                slides = _context.Slides.ToList(),
                pizzas = await _context.Pizzas.ToListAsync()
            };
            return View(pizza);
        }
    }
}
