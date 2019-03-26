using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Models;
using GeolabPortfolio.ViewModels;

namespace GeolabPortfolio.Controllers
{
    public class HomeController : Controller
    {
        GeolabPortfolioDBContext context;

        public HomeController()
        {
            context = new GeolabPortfolioDBContext();
        }
        
        public ActionResult Index()
        {
            ViewBag.tags = context.Tags.ToList();
            return View();
        }

        public ActionResult Author(int id)
        {
            Author author = context.Authors.Where(x => x.Id == id).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            ViewBag.Author = author;
            return View();
        }

        public ActionResult Project(int id)
        {
            Project project = context.Projects.Where(x => x.Id == id).FirstOrDefault();

            if (project == null)
            {
                return RedirectToAction("Index", "Error");
            }

            Author author = context.Authors.Where(x => x.Id == project.AuthorId).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            var mainImage = context.ProjectImages.Where(x => x.IsMain == 1 && x.ProjectId == id).Select(x=>x.ImageUrl).FirstOrDefault();
            var images = context.ProjectImages.Where(x => x.ProjectId == id && x.IsMain == 0).Select(x => x.ImageUrl).ToList();

            List<string> tags = new List<string>();

            foreach (var item in context.ProjectTags.Where(x => x.ProjectId == id).ToList())
            {
                Tag tag = context.Tags.Where(x => x.Id == item.TagId).FirstOrDefault();
                tags.Add(tag.Name);
            }

            HomeProjectDetailsViewModel vm = new HomeProjectDetailsViewModel()
            {
                author = author,
                project = project,
                MainImage = mainImage,
                Images = images,
                Tags = tags
            };

            return View(vm);
        }

        public ActionResult About()
        {
            return View();
        }      
    }
}