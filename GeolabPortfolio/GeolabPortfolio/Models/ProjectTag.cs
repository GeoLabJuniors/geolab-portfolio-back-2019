using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.Models
{
    public class ProjectTag
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TagId { get; set; }
    }
}