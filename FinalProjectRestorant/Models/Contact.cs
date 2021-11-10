using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class Contact
    {
        public int Id  { get; set; }
        [Required]
        public string HowToReach { get; set; }
    }
}
