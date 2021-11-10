using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class Setting
    {
        public int Id { get; set; }

        [Required, StringLength(maximumLength:255)]
        public string Email { get; set; }
        
        public string Eogo { get; set; }
        public string Image { get; set; }
        public string Adress { get; set; }
        [Required,StringLength(maximumLength:50)]
        public string PhoneNumber { get; set; }
        public string SocialMedia { get; set; }
        public string Video { get; set; }
    }
}
