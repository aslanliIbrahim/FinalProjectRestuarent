﻿using FinalProjectRestorant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class BreakFastVM
    {
        public List<Slides> slides { get; set; }
        public List<BreakFast> breakFasts { get; set; }
    }
}
