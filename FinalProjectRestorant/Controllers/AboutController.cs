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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            AboutVM aboutVM = new AboutVM
            {
                slides = _context.Slides.ToList(),
                desserts = await _context.desserts.ToListAsync(),
                about = await _context.Abouts.ToListAsync(),
                aboutCards = await _context.AboutCards.ToListAsync()
            };
            return View(aboutVM);
        }
    }
}
