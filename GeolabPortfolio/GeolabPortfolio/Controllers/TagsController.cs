using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Models;
using GeolabPortfolio.Filters;
using GeolabPortfolio.ViewModels;

namespace GeolabPortfolio.Controllers
{
    [AdminLoginFilter]
    public class TagsController : Controller
    {
        GeolabPortfolioDBContext _context;

        public TagsController()
        {
            _context = new GeolabPortfolioDBContext();
        }

        public ActionResult Index()
        {
            List<Tag> tags = _context.Tags.ToList();
            return View(tags);
        }

        public ActionResult Details(int Id)
        {
            Tag tag = _context.Tags.Where(x => x.Id == Id).FirstOrDefault();

            if (tag == null)
            {
                return RedirectToAction("Index", "Error");
            }

            EditTagViewModel vm = new EditTagViewModel
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return View(vm);
        }
        
        [HttpPost]
        public ActionResult Details(EditTagViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Tag tag = _context.Tags.Where(x => x.Id == vm.Id).FirstOrDefault();

            if (tag == null)
            {
                return RedirectToAction("Index", "Error");
            }

            tag.Name = vm.Name;
            _context.SaveChanges();

            return RedirectToAction("Index", "Tags");
        }

        public ActionResult Create()
        {
            return View(new CreateTagViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreateTagViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _context.Tags.Add(new Tag { Name = vm.Name});
            _context.SaveChanges();

            return RedirectToAction("Index", "Tags");
        }

        [HttpGet]
        public ActionResult Remove(int Id)
        {
            Tag tag = _context.Tags.Where(x => x.Id == Id).FirstOrDefault();

            if (tag == null)
            {
                return RedirectToAction("Index", "Error");
            }

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Dictionary<int, Project> projects = new Dictionary<int, Project>();
                    List<ProjectTag> projectTags = _context.ProjectTags.Where(x => x.TagId == Id).ToList();
                    foreach (ProjectTag projectTag in projectTags)
                    {
                        Project project = _context.Projects.Where(x => x.Id == projectTag.ProjectId).FirstOrDefault();
                        if (project != null)
                        {
                            projects.Add(project.Id, project);
                        }
                        
                    }

                    foreach (KeyValuePair<int, Project> entry in projects)
                    {
                        List<ProjectTag> tags = _context.ProjectTags.Where(x => x.ProjectId == entry.Key).ToList();
                        _context.ProjectTags.RemoveRange(tags);
                        _context.SaveChanges();

                        List<ProjectImage> projectImages = _context.ProjectImages.Where(x => x.ProjectId == entry.Key).ToList();
                        _context.ProjectImages.RemoveRange(projectImages);
                        RemoveFiles(projectImages.Select(x => x.ImageUrl).ToList());
                        _context.SaveChanges();

                        _context.Projects.Remove(entry.Value);
                        _context.SaveChanges();
                    }

                    _context.Tags.Remove(tag);
                    _context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            
            return RedirectToAction("Index", "Tags");
        }

        public void RemoveFile(string fileName)
        {
            var fullPath = Server.MapPath("~/Content/Uploads/" + fileName);
            System.IO.File.Delete(fullPath);
        }

        public void RemoveFiles(List<string> files)
        {
            foreach (string file in files)
            {
                RemoveFile(file);
            }
        }
    }
}