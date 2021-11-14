﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class AboutCard
    {
        public int Id { get; set; }
        [Required,StringLength(maximumLength:255)]
        public string Title { get; set; }
        [Required,StringLength(maximumLength:255)]
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
