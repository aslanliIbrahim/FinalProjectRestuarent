using FinalProjectRestorant.DAL;
using FinalProjectRestorant.Models;
using FinalProjectRestorant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Controllers
{
    public class OurChefController : Controller
    {
        private readonly AppDbContext _context;
        public OurChefController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            OurChefVM chefVM = new OurChefVM
            {
                slides = _context.Slides.ToList(),
                Ourchef = await _context.ourChefs.ToListAsync()

            };

            return View(chefVM);
        }
    }
}
