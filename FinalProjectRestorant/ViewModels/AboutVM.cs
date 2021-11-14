using FinalProjectRestorant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class AboutVM
    {
        public List<Slides> slides { get; set; }
        public List<Desserts> desserts { get; set; }
        public List<About> about { get; set; }
        public List<AboutCard> aboutCards { get; set; }
    }
}
