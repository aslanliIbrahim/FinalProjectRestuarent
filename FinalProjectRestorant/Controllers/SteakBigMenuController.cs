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
    public class SteakBigMenuController : Controller
    {
        private readonly AppDbContext _context;
        public SteakBigMenuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            decimal pageItemCount = 3;
            decimal b = Math.Ceiling(_context.SteakBigMenus.Count() / pageItemCount);
            ViewBag.pageCount =Convert.ToInt32(b);
            ViewBag.ActivePage = page;


            List<SteakBigMenu> steakBigMenus = await _context.SteakBigMenus.OrderBy(st=>st.Id)
                                                                           .Skip((page - 1) * (int)pageItemCount)
                                                                           .Take((int)pageItemCount)
                                                                           .ToListAsync();


            SteakBigMenuVM steakBigMenu = new SteakBigMenuVM
            {
                slides = _context.Slides.ToList(),
                steakBigMenus = steakBigMenus
            };
            return View(steakBigMenu);
        }
    }
}
