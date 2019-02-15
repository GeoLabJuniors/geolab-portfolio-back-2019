using System.ComponentModel.DataAnnotations;

namespace GeolabPortfolio.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "შეავსეთ ელ.ფოსტის ველი")]
        [EmailAddress(ErrorMessage = "არ არის სწორი ელ.ფოსტა ფორმატი")]
        public string Email { get; set; }

        [Required(ErrorMessage = "შეავსეთ პაროლის ველი")]
        public string Password { get; set; }
    }
}