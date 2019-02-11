using System;
using System.ComponentModel.DataAnnotations;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class CreateTagViewModel
    {
        [Required(ErrorMessage = "შეავსეთ ველი")]
        [MinLength(3,ErrorMessage ="თეგი უნდა შეიცავდეს მინიმუმ 3 სიმბოლოს!")]
        [IsTagUnique]
        public String Name { get; set; }
    }
}