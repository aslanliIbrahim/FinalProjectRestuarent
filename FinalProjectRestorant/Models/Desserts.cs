using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class Desserts
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
        [Required, StringLength(maximumLength: 100)]
        public string BigMenuFoodName { get; set; }
        [Required, StringLength(maximumLength: 225)]
        public string Ingredient { get; set; }
        [Required, StringLength(maximumLength: 100)]
        public string Servis { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Pieces { get; set; }
    }
}
