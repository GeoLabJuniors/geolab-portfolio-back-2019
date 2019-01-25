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

            return RedirectToAction("Details", "Tags", new { id = vm.Id });
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
    }
}