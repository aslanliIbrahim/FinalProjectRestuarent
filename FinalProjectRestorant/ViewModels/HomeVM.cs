using FinalProjectRestorant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class HomeVM
    {
        public List<Slides> Slides { get; set; }
        public List<About> Abouts { get; set; }
        public List<HomeCard> HomeCards { get; set; }
    }
}
