using GeolabPortfolio.Models;
using System.Data.Entity;

namespace GeolabPortfolio.Database
{
    public class GeolabPortfolioDBContext : DbContext
    {
        public GeolabPortfolioDBContext():base("Data Source=.;Initial Catalog=GeolabPortfolio;Integrated Security=True")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
    }
}