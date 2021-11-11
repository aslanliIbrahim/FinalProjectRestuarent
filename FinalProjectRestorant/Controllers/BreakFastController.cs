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
    public class BreakFastController : Controller
    {
        private readonly AppDbContext _context;
        public BreakFastController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            BreakFastVM breakFastVM = new BreakFastVM
            {
                slides = _context.Slides.ToList(),
                breakFasts = await _context.breakFasts.ToListAsync()
            };
            return View(breakFastVM);
        }
    }
}
