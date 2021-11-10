using FinalProjectRestorant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class ContactVM
    {
        public string HowToReach { get; set; }
        [Required, StringLength(maximumLength: 20)]
        public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string SocialMedia { get; set; }

        public Contact contacts { get; set; }
        public Sms sms { get; set; }
        public Setting setting { get; set; }

    }
}
