using System.Collections.Generic;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.ViewModels
{
    public class UserProjectViewModel
    {
        public List<Project> Projects { get; set; }
        public Dictionary<int, string> TagDictionary;
        public List<ProjectTag> ProjectTags;
    }
}