using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class HomeProjectDetailsViewModel
    {
        public Author author { get; set; }
        public Project project { get; set; }
        public string MainImage { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
    }
}