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
    public class SteakBigMenuController : Controller
    {
        private readonly AppDbContext _context;
        public SteakBigMenuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            SteakBigMenuVM steakBigMenu = new SteakBigMenuVM
            {
                slides = _context.Slides.ToList(),
                steakBigMenus = await _context.SteakBigMenus.ToListAsync()
            };
            return View(steakBigMenu);
        }
    }
}
