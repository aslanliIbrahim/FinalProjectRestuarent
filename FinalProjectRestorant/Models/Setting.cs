using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class Setting
    {
        public int Id { get; set; }

        [Required, StringLength(maximumLength:255)]
        public string Email { get; set; }
        [Required]
        public string Eogo { get; set; }
        public string Image { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
        public string Adress { get; set; }
        [Required,StringLength(maximumLength:50)]
        public string PhoneNumber { get; set; }
        public string SocialMedia { get; set; }
        
    }
}
