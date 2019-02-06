using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }
        public string BehanceLink { get; set; }
        public string LinkedinLink { get; set; }
        public string GithubLink { get; set; }
        public string DribbleLink { get; set; }
    }
}