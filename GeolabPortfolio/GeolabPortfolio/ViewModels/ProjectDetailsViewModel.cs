using System.Collections.Generic;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; }
        public Author Author { get; set; }
        public List<ProjectImage> ProjectImages { get; set; }
        public List<ProjectTag> Tags { get; set; }
        public Dictionary<int,string> TagDictionary { get; set; }
    }
}