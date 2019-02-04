using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Filters;
using GeolabPortfolio.Models;
using GeolabPortfolio.ViewModels;

namespace GeolabPortfolio.Controllers
{
    [AdminLoginFilter]
    public class AuthorController : Controller
    {
        GeolabPortfolioDBContext _context;

        public AuthorController()
        {
            _context = new GeolabPortfolioDBContext();
        }

        public ActionResult Index()
        {
            List<Author> authors = _context.Authors.ToList();    
            return View(authors);
        }

        public ActionResult Create()
        {
            CreateAuthorViewModel vm = new CreateAuthorViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CreateAuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string []allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };

             string pic = Guid.NewGuid().ToString() + Path.GetFileName(vm.Image.FileName);

             bool find = false;

             foreach(string extension in allowedExtensions)
             {
                 if (pic.Contains(extension))
                 {
                    find = true;
                    break;
                 }
             }

             if (!find)
             {
                 ModelState.AddModelError("Errors", "არასწორი ფორმატი");
                 return View(vm);
             }

             string path = Path.Combine(Server.MapPath("~/Content/uploads/"), pic);
             // file is uploaded
             vm.Image.SaveAs(path);
             

            _context.Authors.Add(new Author
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Image = pic,
                BehanceLink = vm.BehanceLink,
                GithubLink = vm.GithubLink,
                LinkedinLink = vm.LinkedinLink,
                DribbleLink = vm.DribbleLink
            });

            _context.SaveChanges();

            return RedirectToAction("Index", "Author");
        }

        public ActionResult Details(int Id)
        {
            Author author = _context.Authors.Where(x => x.Id == Id).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            Dictionary<int, string> TagDictionary = new Dictionary<int, string>();

            foreach (Tag tag in _context.Tags.ToList())
            {
                TagDictionary.Add(tag.Id, tag.Name);
            }

            AuthorDetailsViewModel vm = new AuthorDetailsViewModel
            {
                author = author,
                Projects = _context.Projects.Where(x=>x.AuthorId == Id).ToList(),
                TagDictionary = TagDictionary,
                ProjectTags = _context.ProjectTags.ToList()
            };

            return View(vm);
        }

        public ActionResult Edit(int Id)
        {
            Author author = _context.Authors.Where(x => x.Id == Id).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            EditAuthorViewModel vm = new EditAuthorViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                FileName = author.Image,
                GithubLink = author.GithubLink,
                LinkedinLink = author.LinkedinLink,
                BehanceLink = author.BehanceLink,
                DribbleLink = author.DribbleLink
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(EditAuthorViewModel vm)
        {

            Author author = _context.Authors.Where(x => x.Id == vm.Id).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Image != null)
            {
                //remove old image
                var fullPath = Server.MapPath("~/Content/Uploads/" + author.Image);
                System.IO.File.Delete(fullPath);
                // upload new one

                string[] allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                string pic = Guid.NewGuid().ToString() + Path.GetFileName(vm.Image.FileName);
                bool find = false;
                foreach (string extension in allowedExtensions)
                {
                    if (pic.Contains(extension))
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    ModelState.AddModelError("Errors", "არასწორი ფორმატი");
                    return View(vm);
                }

                string path = Path.Combine(Server.MapPath("~/Content/Uploads/"), pic);
                // file is uploaded
                vm.Image.SaveAs(path);
                vm.FileName = pic;
                author.Image = pic;
            }

            
            author.FirstName = vm.FirstName;
            author.LastName = vm.LastName;
            author.GithubLink = vm.GithubLink;
            author.LinkedinLink = vm.LinkedinLink;
            author.DribbleLink = vm.DribbleLink;
            author.BehanceLink = vm.BehanceLink;

            _context.SaveChanges();

            return RedirectToAction("Index", "Author");
        }

        public ActionResult Remove(int Id)
        {
            Author author = _context.Authors.Where(x => x.Id == Id).FirstOrDefault();

            if (author == null)
            {
                return RedirectToAction("Index", "Error");
            }

            //remove author image from ftp
            var fullPath = Server.MapPath("~/Content/Uploads/" + author.Image);
            System.IO.File.Delete(fullPath);

            _context.Authors.Remove(author);

            // remove project and tags here
            
            _context.SaveChanges();


            return RedirectToAction("Index", "Author");
        }
    }
}