using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class Slides
    {
        public int Id { get; set; }
        
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required, StringLength(maximumLength:100)]
        public string RestaurantName { get; set; }
        [Required,StringLength(maximumLength:255)]
        public string Description { get; set; }
        [Required,StringLength(maximumLength:255)]
        public string Saw { get; set; }
    }
}
