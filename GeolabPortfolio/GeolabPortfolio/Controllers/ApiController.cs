using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Extensions;
using GeolabPortfolio.Models;
using GeolabPortfolio.ViewModels;

namespace GeolabPortfolio.Controllers
{
    public class ApiController : Controller
    {
        GeolabPortfolioDBContext context;

        public ApiController()
        {
            context = new GeolabPortfolioDBContext();
        }
        
        [HttpGet]
        public JsonResult GetProjectByAuthorId(int id)
        {
            Author author = context.Authors.Where(x => x.Id == id).FirstOrDefault();
            List<ProjectList> projectList = new List<ProjectList>();

            if (author == null)
            {
                return Json(projectList, JsonRequestBehavior.AllowGet);
            }

            var command = "exec FilterProjectByAuthorId " + id;
            projectList = context.Database.SqlQuery<ProjectList>(command).ToList();

            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FilterProjects(string Tags)
        {
            var command = "exec FilterProjectByTags '" + Tags.Replace(' ', ',') + "'";
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