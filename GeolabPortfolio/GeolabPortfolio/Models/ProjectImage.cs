namespace GeolabPortfolio.Models
{
    public class ProjectImage
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ImageUrl { get; set; }
        public int IsMain { get; set; }
    }
}