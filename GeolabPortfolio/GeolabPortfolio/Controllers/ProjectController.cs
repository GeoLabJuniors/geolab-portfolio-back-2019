using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeolabPortfolio.ViewModels;
using GeolabPortfolio.Database;
using GeolabPortfolio.Models;
using System.Collections.Generic;
using GeolabPortfolio.Filters;

namespace GeolabPortfolio.Controllers
{
//    [AdminLoginFilter]
    public class ProjectController : Controller
    {
        GeolabPortfolioDBContext _context;

        public ProjectController()
        {
            _context = new GeolabPortfolioDBContext();
        }

        public ActionResult Index()
        {
            var courses = (from project in _context.Projects.ToList()
                          join authors in _context.Authors.ToList() on project.AuthorId equals authors.Id
                          join projectimages in _context.ProjectImages.ToList() on project.Id equals projectimages.ProjectId
                          where projectimages.IsMain == 1
                          select new  ProjectListViewModel
                          {
                              Id = project.Id,
                              Name = project.Name,
                              Image = projectimages.ImageUrl,
                              AuthorFullName = authors.FirstName + " " + authors.LastName
                          }).ToList();

            return View(courses);
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

        public ActionResult Create()
        {
            CreateProjectViewModel vm = new CreateProjectViewModel();
            vm.Authors = _context.Authors.ToList();
            vm.Tags = _context.Tags.ToList();
            vm.Published = DateTime.Now;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CreateProjectViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Authors = _context.Authors.ToList();
                vm.Tags = _context.Tags.ToList();
                vm.Published = DateTime.Now;
                return View(vm);
            }

            //validate files

            foreach (var file in vm.Photos)
            {
                if (!ValidateFile(file))
                {
                    ModelState.AddModelError("error", "არასწორია ფაილის ფორმატი");
                    vm.Authors = _context.Authors.ToList();
                    vm.Tags = _context.Tags.ToList();
                    vm.Published = DateTime.Now;
                    return View(vm);
                }
            }


            //transaction
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {   
                    // add project
                    Project project = new Project
                    {
                        AuthorId = vm.AuthorId,
                        Description = vm.Description,
                        Name = vm.Name,
                        Published = DateTime.Now
                    };

                    _context.Projects.Add(project);
                    _context.SaveChanges();

                    // add tags
                    foreach (int tagId in vm.TagIds)
                    {
                        ProjectTag tag = new ProjectTag
                        {
                            ProjectId = project.Id,
                            TagId = tagId
                        };
                        _context.ProjectTags.Add(tag);
                    }
                    _context.SaveChanges();

                    //upload files
                    for (int i = 0; i < vm.Photos.Length; i++)
                    {
                        ProjectImage projectImage = new ProjectImage
                        {
                            ProjectId = project.Id,
                            IsMain = (vm.Primary == i) ? 1 : 0,
                            ImageUrl = UploadFile(vm.Photos[i])
                        };
                        _context.ProjectImages.Add(projectImage);
                    }

                    _context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("Index", "Project");
        }
        
        [HttpGet]
        public ActionResult Remove(int Id)
        {
            Project project = _context.Projects.Where(x => x.Id == Id).FirstOrDefault();

            if (project == null)
            {
                return RedirectToAction("Index", "Error");
            }

            //remove tags
            foreach (ProjectTag tag in _context.ProjectTags.Where(x => x.ProjectId == Id).ToList())
            {
                _context.ProjectTags.Remove(tag);
            }
            _context.SaveChanges();

            //remove images
            foreach (ProjectImage image in _context.ProjectImages.Where(x => x.ProjectId == Id).ToList())
            {
                RemoveFile(image.ImageUrl);
                _context.ProjectImages.Remove(image);
            }
            _context.SaveChanges();
            
            _context.Projects.Remove(project);

            _context.SaveChanges();

            return RedirectToAction("Index", "Project");
        }

        public ActionResult Details(int Id)
        {
            Project project = _context.Projects.Where(x => x.Id == Id).FirstOrDefault();

            if (project == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            Author author = _context.Authors.Where(x => x.Id == project.AuthorId).FirstOrDefault();
            List<ProjectTag> tags = _context.ProjectTags.Where(x => x.ProjectId == Id).ToList();
            List<ProjectImage> images = _context.ProjectImages.Where(x => x.ProjectId == Id).ToList();

            Dictionary<int, string> tagDictionary = new Dictionary<int, string>();

            foreach (var tag in _context.Tags.ToList())
            {
                tagDictionary.Add(tag.Id, tag.Name);
            }

            ProjectDetailsViewModel vm = new ProjectDetailsViewModel
            {
                Project = project,
                Tags = tags,
                ProjectImages = images,
                Author = author,
                TagDictionary = tagDictionary
            };

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            Project project = _context.Projects.Where(x => x.Id == id).FirstOrDefault();

            if (project == null)
            {
                return RedirectToAction("Index", "Error");
            }

            HashSet<int> TagHash = new HashSet<int>();

            foreach (var tag in _context.ProjectTags.Where(x => x.ProjectId == id).ToList())
            {
                TagHash.Add(tag.TagId);
            }

            EditProjectViewModel vm = new EditProjectViewModel
            {
                Id = project.Id,
                AuthorId = project.AuthorId,
                Name = project.Name,
                Description = project.Description,
                Published = project.Published,
                Authors = _context.Authors.ToList(),
                Tags = _context.Tags.ToList(),
                TagHash = TagHash,
                ProjectImages = _context.ProjectImages.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(EditProjectViewModel vm)
        {
            Project project = _context.Projects.Where(x => x.Id == vm.Id).FirstOrDefault();

            if(project == null)
            {
                return RedirectToAction("Index", "Error");
            }

            if (!ModelState.IsValid)
            {
                HashSet<int> TagHash = new HashSet<int>();

                foreach (var tag in _context.ProjectTags.Where(x => x.ProjectId == project.Id).ToList())
                {
                    TagHash.Add(tag.TagId);
                }

                vm.Authors = _context.Authors.ToList();
                vm.Tags = _context.Tags.ToList();
                vm.TagHash = TagHash;
                vm.ProjectImages = _context.ProjectImages.ToList();
                
                return View(vm);
            }

            project.Name = vm.Name;
            project.Description = vm.Description;
            project.AuthorId = vm.AuthorId;
            project.Published = vm.Published;
            _context.SaveChanges();

            HashSet<int> currentTags = new HashSet<int>();
            HashSet<int> selectedTags = new HashSet<int>();

            foreach (ProjectTag tag in _context.ProjectTags.Where(x => x.ProjectId == vm.Id).ToList())
            {
                currentTags.Add(tag.TagId);
            }

            foreach (int id in vm.SelectedTags)
            {
                selectedTags.Add(id);
            }
            
            foreach (ProjectTag ptag in _context.ProjectTags.Where(x => x.ProjectId == vm.Id).ToList())
            {
                if (!selectedTags.Contains(ptag.TagId))
                {
                    _context.ProjectTags.Remove(ptag);
                }
            }

            foreach (int TagId in vm.SelectedTags)
            {
                if (!currentTags.Contains(TagId))
                {
                    _context.ProjectTags.Add(new ProjectTag { ProjectId = vm.Id, TagId = TagId });
                }
            }

            // images upload new ones
            foreach (var photo in vm.Photos)
            {
                _context.ProjectImages.Add(new ProjectImage
                {
                    ProjectId = vm.Id,
                    IsMain = 0,
                    ImageUrl = UploadFile(photo)
                });
            }



            _context.SaveChanges();

            return RedirectToAction("Index", "Project");
        }

        public void UpdateTags()
        {

        }

        /*
            additional function for working files 
        */

        public string UploadFile(HttpPostedFileBase file)
        {
            string pic = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/uploads/"), pic);
            file.SaveAs(path);
            return pic;
        }

        public bool ValidateFile(HttpPostedFileBase file)
        {
            string[] AllowedFileExtensions = new string[] { ".png", ".jpg" };
            var filename = file.FileName;
            return AllowedFileExtensions.Contains(filename.Substring(filename.LastIndexOf('.')));
        }

        public void RemoveFile(string fileName)
        {
            var fullPath = Server.MapPath("~/Content/Uploads/" + fileName);
            System.IO.File.Delete(fullPath);
        }
    }
}