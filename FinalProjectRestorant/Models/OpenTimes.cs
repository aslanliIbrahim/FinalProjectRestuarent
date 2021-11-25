using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class OpenTimes
    {
        public int Id { get; set; }
       
        [Required]
        public string Hours { get; set; }
        //[Required]
        //public string DinnerHours { get; set; }
        [Required]
        public string MealTime { get; set; }
        [Required]
        public string Day { get; set; }

    }
}
