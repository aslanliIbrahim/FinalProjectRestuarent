using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Slides = _context.Slides.ToList(),
                Abouts = _context.Abouts.ToList(),
                HomeCards = _context.HomeCards.ToList(),
                SteakBigMenus =await _context.SteakBigMenus.ToListAsync(),
                BreakFasts = _context.breakFasts.ToList(),
                Pizzas = _context.Pizzas.ToList(),
                Starters = _context.starters.ToList(),
                OpenTimes = _context.OpenTimes.ToList()
            };
            
            return View(homeVM);
        }
    }
}
