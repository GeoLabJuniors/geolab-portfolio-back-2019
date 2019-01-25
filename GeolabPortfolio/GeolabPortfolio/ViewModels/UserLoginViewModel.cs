using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "შეავსეთ ელ.ფოსტის ველი")]
        [DataType(DataType.EmailAddress,ErrorMessage ="არ არის სწორი ელ.ფოსტა ფორმატი")]
        public string Email { get; set; }

        [Required(ErrorMessage = "შეავსეთ პაროლის ველი")]
        public string Password { get; set; }
    }
}