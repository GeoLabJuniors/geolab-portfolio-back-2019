using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public Author author { get; set; }
        public List<ProjectListViewModel> Courses { get; set; }
    }
}