using System.ComponentModel.DataAnnotations;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class EditTagViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "შეავსეთ ველი")]
        [MinLength(3,ErrorMessage = "მინიმუმ 3 სიმბოლო")]
        [IsTagUnique]
        public string Name { get; set; }
    }
}