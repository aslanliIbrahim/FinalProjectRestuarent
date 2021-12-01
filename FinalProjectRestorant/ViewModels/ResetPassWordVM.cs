using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectRestorant.ViewModels
{
    public class ResetPassWordVM
    {
        public string Id { get; set; }
        public string Token { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassWord { get; set; }
    }
}
