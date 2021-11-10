using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class LoginVM
    {
        [Required, StringLength(maximumLength: 20)]
        public string UserName { get; set; }
      
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
