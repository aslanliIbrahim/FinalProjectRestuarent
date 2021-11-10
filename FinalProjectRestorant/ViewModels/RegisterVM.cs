using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class RegisterVM
    {
        [Required,StringLength(maximumLength:100)]
        public string FullName { get; set; }
        [Required, StringLength(maximumLength: 20)]
        public string UserName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string  Password { get; set; }
        [Required,DataType(DataType.Password), Compare(nameof(Password),ErrorMessage = "Confirm PassWord and Password are not match...")]
        public string ConfirmPasssword { get; set; }
    }
}
