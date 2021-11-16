using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class Order
    {
        public int Id{ get; set; }
        public List<SteakBigMenu> SteakBigMenus { get; set; } 
        public int SteakBigId { get; set; }
        public List<BreakFast> BreakFasts { get; set; }
        public int PreakFastsId { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public int PizzaId { get; set; }
        public List<Starters> Starters { get; set; }
        public int StartersId { get; set; }
        public List<Desserts> Desserts { get; set; }
        public int DessertId { get; set; }
        public List<Drinks> Drinks { get; set; }
        public int DrinksId { get; set; }
    }
}
