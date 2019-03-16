using System.Linq;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Models;

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

        public ActionResult About()
        {
            return View();
        }      
    }
}