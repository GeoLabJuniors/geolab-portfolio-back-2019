using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "შეავსე ელ.ფოსტის ფელი")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "შეავსე ძველი პაროლის ველი")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "პაროლი უნდა შეიცავდეს მინიმუმ 6 სიმბოლოს!")]
        [IsAdminPasswordRight]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "შეავსე ახალი პაროლის ველი")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "პაროლი უნდა შეიცავდეს მინიმუმ 6 სიმბოლოს!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "გაიმეორე ახალი პაროლი")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "პაროლი უნდა შეიცავდეს მინიმუმ 6 სიმბოლოს!")]
        [Compare(nameof(NewPassword), ErrorMessage = "არ ემთხვევა ახალ შეყვანილ პაროლს!")]
        public string RepeatPassword { get; set; }
    }
}