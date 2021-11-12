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
    public class DessertsController : Controller
    {
        private readonly AppDbContext _context;
        public DessertsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            DessetsVM dessets = new DessetsVM
            {
                slides = _context.Slides.ToList(),
                desserts =await _context.desserts.ToListAsync()
            };
            return View(dessets);
        }
    }
}
