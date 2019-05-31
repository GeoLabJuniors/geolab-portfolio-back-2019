using GeolabPortfolio.Models;
using System.Data.Entity;

namespace GeolabPortfolio.Database
{
    public class GeolabPortfolioDBContext : DbContext
    {
        public GeolabPortfolioDBContext():base("Data Source=5.175.2.145;Initial Catalog=GeolabPortfolio;user id=geolabBase;password=geolab#6;persist security info=True;multipleactiveresultsets=True")
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