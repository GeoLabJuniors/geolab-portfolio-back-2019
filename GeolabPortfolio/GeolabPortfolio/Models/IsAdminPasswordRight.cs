using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GeolabPortfolio.Database;

namespace GeolabPortfolio.Models
{
    public class IsAdminPasswordRight:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GeolabPortfolioDBContext _context = new GeolabPortfolioDBContext();

            var user = _context.Users.ToList().First();

            string passwordHashed = PasswordHasher.GetHash((string)value);

            if (user.Password == passwordHashed)
                return ValidationResult.Success;
            else
                return new ValidationResult("მოხმარებლის პაროლი არასწორია");
        }
    }
}