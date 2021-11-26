using FinalProjectRestorant.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AboutChefController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public AboutChefController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
