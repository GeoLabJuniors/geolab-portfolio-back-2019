using System.Linq;
using System.Web.Mvc;
using GeolabPortfolio.Database;
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

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public JsonResult FilterProjects(string Tags)
        {
            Tags = Tags.Replace(' ', ',');

            var projectList = context.Database
                .SqlQuery<ProjectListViewModel>("exec FilterProjectByTags '" + Tags + "'").ToList();

            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ProjectList()
        {
            var items = context.Database.SqlQuery<ProjectListViewModel>("exec GetProjectList").ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}