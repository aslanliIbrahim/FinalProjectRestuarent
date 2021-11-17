using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class AdminOrder
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string NameOfFood { get; set; }
        public string Image { get; set; }
    }
}
