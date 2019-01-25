﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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