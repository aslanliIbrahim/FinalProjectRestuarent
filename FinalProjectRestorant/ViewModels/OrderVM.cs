using FinalProjectRestorant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class OrderVM
    {
        public List<Slides> Slides { get; set; }
        public List<SteakBigMenu> SteakBigMenus { get; set; }
        public List<BreakFast> BreakFasts { get; set; }
        public  List<Pizza> Pizzas { get; set; }
        public List<Starters> Starters { get; set; }
        public List<Desserts> Desserts { get; set; }
        public List<Drinks> Drinks { get; set; }
        public SteakBigMenu Order { get; set; }
    }
}
