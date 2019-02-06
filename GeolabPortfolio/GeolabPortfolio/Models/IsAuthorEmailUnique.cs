using GeolabPortfolio.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.Models
{
    public class IsAuthorEmailUnique:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GeolabPortfolioDBContext _context = new GeolabPortfolioDBContext();
            
            var author = _context.Authors.Where(x => x.Email == (string)value).FirstOrDefault();

            if (author == null)
                return ValidationResult.Success;
            else
                return new ValidationResult("ასეთი ელ-ფოსტით უკვე არსებობს!");
        }
    }

}