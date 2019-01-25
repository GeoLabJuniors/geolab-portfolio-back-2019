using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.ViewModels
{
    public class EditAuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "შეავსეთ სახელის ველი")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "შეავსეთ გვარის ველი")]
        public string LastName { get; set; }
        
        
        public HttpPostedFileBase Image { get; set; }
        public string FileName { get; set; }

        public string BehanceLink { get; set; }
        public string DribbleLink { get; set; }
        public string LinkedinLink { get; set; }
        public string GithubLink { get; set; }

    }
}