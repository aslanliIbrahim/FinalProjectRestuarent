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
        public SteakBigMenu SteakBigMenus { get; set; }
        public BreakFast BreakFasts { get; set; }
        public  Pizza Pizzas { get; set; }
        public Starters Starters { get; set; }
        public Desserts Desserts { get; set; }
        public Drinks Drinks { get; set; }
        public SteakBigMenu Order { get; set; }
        public AdminOrder AdminOrders { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string FoodName { get; set; }
    }
}
