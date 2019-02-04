﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime Published { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}