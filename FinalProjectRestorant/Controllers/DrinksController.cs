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
    public class DrinksController : Controller
    {

        private readonly AppDbContext _context;
        public DrinksController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            DrinkVm drink = new DrinkVm
            {
                slides = _context.Slides.ToList(),
                drinks = await _context.drinks.ToListAsync()
            };
            return View(drink);
        }
    }
}
