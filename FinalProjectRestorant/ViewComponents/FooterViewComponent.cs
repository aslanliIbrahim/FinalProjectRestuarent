using FinalProjectRestorant.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        public AppDbContext _context { get; }
        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _context.Settings.FirstOrDefault();
            return  View(await Task.FromResult(model));
        }
    }
}
