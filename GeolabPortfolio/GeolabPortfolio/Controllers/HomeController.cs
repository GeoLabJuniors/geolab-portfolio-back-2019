using System.Linq;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Extensions;
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

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public JsonResult FilterProjects(string Tags)
        {
            var command = "exec FilterProjectByTags '" + Tags.Replace(' ',',') + "'";
            var projectList = context.Database.SqlQuery<ProjectList>(command).ToList();
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ProjectList()
        {
            var command = "exec GetProjectList";
            var items = context.Database.SqlQuery<ProjectList>(command).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FilterProjectListByString(string words)
        {
            ProjectFilter filter = new ProjectFilter();
            return Json(filter.ProjectFilterByText(words), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FilterProjectListByStringAndTags(string words, string tags)
        {
            ProjectFilter filter = new ProjectFilter();
            return Json(filter.ProjectFilterByTextAndTags(words, tags), JsonRequestBehavior.AllowGet);
        }        
    }
}