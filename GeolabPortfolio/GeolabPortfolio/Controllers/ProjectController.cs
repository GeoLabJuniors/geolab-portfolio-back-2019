using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeolabPortfolio.ViewModels;
using GeolabPortfolio.Database;
using GeolabPortfolio.Models;
using GeolabPortfolio.Filters;
using System.Collections.Generic;

namespace GeolabPortfolio.Controllers
{
    [AdminLoginFilter]
    public class ProjectController : Controller
    {

        GeolabPortfolioDBContext _context;

        public ProjectController()
        {
            _context = new GeolabPortfolioDBContext();
        }

        public ActionResult Index()
        {
            return View(_context.Projects.ToList());
        }

        public ActionResult UserProjects(int Id)
        {
            Author author = _context.Authors.Where(x => x.Id == Id).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            List<Project> projects = _context.Projects.Where(x => x.AuthorId == Id).ToList();

            Dictionary<int, string> TagDictionary = new Dictionary<int, string>();

            foreach (Tag tag in _context.Tags.ToList())
            {
                TagDictionary.Add(tag.Id, tag.Name);
            }

            UserProjectViewModel vm = new UserProjectViewModel
            {
                Projects = projects,
                ProjectTags = _context.ProjectTags.ToList(),
                TagDictionary = TagDictionary
            };

            return View(vm);
        }

        public ActionResult Create(int Id)
        {
            Author author = _context.Authors.Where(x => x.Id == Id).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            CreateProjectViewModel vm = new CreateProjectViewModel
            {
                AuthorId = Id,
                Tags = _context.Tags.ToList(),
                Published = DateTime.Now
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase Image, CreateProjectViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Tags = _context.Tags.ToList();
                return View(vm);
            }

            string[] allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };

            string pic = Guid.NewGuid().ToString() + Path.GetFileName(Image.FileName);

            bool find = false;

            foreach (string extension in allowedExtensions)
            {
                if (pic.Contains(extension))
                {
                    find = true;
                }
            }

            if (!find)
            {
                ModelState.AddModelError("errors", "არასწორი ფორმატი");
                vm.Tags = _context.Tags.ToList();
                return View();
            }

            string path = Path.Combine(Server.MapPath("~/Content/uploads/"), pic);
            // file is uploaded
            Image.SaveAs(path);

            Project project = new Project
            {
                AuthorId = vm.AuthorId,
                Name = vm.Name,
                Image = pic,
                Description = vm.Description,
                Published = DateTime.Now
            };

            _context.Projects.Add(project);
            _context.SaveChanges();
            

            foreach (int id in vm.TagIds)
            {
                _context.ProjectTags.Add(new ProjectTag
                {
                    TagId = id,
                    ProjectId = project.Id
                });
            }

            _context.SaveChanges();
            
            return RedirectToAction("Details", "Author", new { Id = vm.AuthorId });
        }
    }
}