﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class EditProjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "აირჩიეთ ავტორი")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "შეავსეთ სახელი")]
        public string Name { get; set; }

        [Required(ErrorMessage = "აღწერა დაამატეთ")]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "თარიღი დაამატეთ")]
        public DateTime Published { get; set; }
        
        public HttpPostedFileBase[] Photos { get; set; } // new image 

        public int[] SelectedTags { get; set; }

        public List<Author> Authors { get; set; }
        public List<Tag> Tags { get; set; }
        public HashSet<int> TagHash { get; set; }
        public List<ProjectImage> ProjectImages { get; set; }
    }
}