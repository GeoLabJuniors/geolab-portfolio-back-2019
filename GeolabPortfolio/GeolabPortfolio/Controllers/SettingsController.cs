using System.Linq;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Models;
using GeolabPortfolio.ViewModels;
using GeolabPortfolio.Filters;

namespace GeolabPortfolio.Controllers
{
    [AdminLoginFilter]
    public class SettingsController : Controller
    {
        GeolabPortfolioDBContext _context;

        public SettingsController()
        {
            _context = new GeolabPortfolioDBContext();
        }

        public ActionResult Index()
        {
            User user = _context.Users.ToList().First();

            EditUserViewModel vm = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(EditUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            User user = _context.Users.ToList().First();

            user.Email = vm.Email;
            user.Password = PasswordHasher.GetHash(vm.NewPassword);

            _context.SaveChanges();

            return RedirectToAction("Index", "Settings");
        }
    }
}