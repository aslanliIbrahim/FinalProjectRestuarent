using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class HomeCard
    {
        public int Id { get; set; }
        [Required,StringLength(maximumLength:500)]
        public string Title { get; set; }
        [Required, StringLength(maximumLength:500)]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required,NotMapped]
        public IFormFile Photo { get; set; }
        [Required,StringLength(maximumLength:500)]
        public string TypeOfFood { get; set; }
    }
}
