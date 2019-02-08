using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class CreateProjectViewModel
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "სახელის ველი ცარიელია")]
        public string Name { get; set; }
        
        [Required]
        public DateTime Published { get; set; }

        [Required(ErrorMessage = "აღწერის ველი ცარიელია")]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "აირჩიეთ თეგები")]
        public int[] TagIds { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Author> Authors { get; set; }

        [Required(ErrorMessage = "ატვირთეთ ფოტოები")]
        public HttpPostedFileBase[] Photos { get; set; }

        public int Primary { get; set; }
    }
}