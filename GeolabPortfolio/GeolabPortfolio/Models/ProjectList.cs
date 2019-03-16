using System;

namespace GeolabPortfolio.Models
{
    public class ProjectList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Published { get; set; }
        public int AuthorId { get; set; }
    }
}