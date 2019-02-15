using GeolabPortfolio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.ViewModels
{
    public class CreateAuthorViewModel
    {
        [Required(ErrorMessage = "სახელის ველი შეავსეთ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "გვარის ველი შეავსეთ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ელ.ფოსტის ველი შეავსეთ")]
        [IsAuthorEmailUnique]
        [EmailAddress(ErrorMessage = "არ არის სწორი ელ.ფოსტა ფორმატი")]
        public string Email { get; set; }

        [Required(ErrorMessage = "სურათი საჭიროა")]
        public HttpPostedFileBase Image { get; set; }

        public string BehanceLink { get; set; }
        
        public string LinkedinLink { get; set; }
        
        public string GithubLink { get; set; }
        
        public string DribbleLink { get; set; }
    }
}