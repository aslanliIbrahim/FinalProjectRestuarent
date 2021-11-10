using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.Models
{
    public class Sms
    {
        public int Id { get; set; }
        [Required, StringLength(maximumLength:50)]
        public string  Name { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,StringLength(maximumLength:500)]
        public string Message { get; set; }

    }
}
