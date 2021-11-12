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
    public class StartersController : Controller
    {
        private readonly AppDbContext _context;
        public StartersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            StarTersVM starTers = new StarTersVM
            {
                slides = _context.Slides.ToList(),
                Starters =await _context.starters.ToListAsync()
            };
            return View(starTers);
        }
    }
}
