using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class OurChef
    {
        public int Id { get; set; }
        [StringLength(maximumLength:255)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required, StringLength(maximumLength:100)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength:200)]
        public string Face { get; set; }
        [Required,StringLength(maximumLength:200)]
        public string Insta { get; set; }
        [Required, StringLength(maximumLength:200)]
        public string Twit { get; set; }
        [Required, StringLength(maximumLength:50)]
        public string AboutChef { get; set; }
        [Required, StringLength(maximumLength:30)]
        public string Position { get; set; }
    }
}
