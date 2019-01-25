using GeolabPortfolio.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.Models
{
    public class IsTagUnique:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GeolabPortfolioDBContext _context = new GeolabPortfolioDBContext();

            var tag = _context.Tags.Where(x => x.Name.ToString() == ((string)value)).FirstOrDefault();

            if (tag == null)
                return ValidationResult.Success;
            else
                return new ValidationResult("ასეთი თეგი უკვე არსებობს");
        }
    }
    
}