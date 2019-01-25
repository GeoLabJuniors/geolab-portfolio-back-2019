using System.Linq;
using System.Web.Mvc;
using GeolabPortfolio.Database;
using GeolabPortfolio.Filters;
using GeolabPortfolio.Models;
using GeolabPortfolio.ViewModels;

namespace GeolabPortfolio.Controllers
{
    public class AccountController : Controller
    {
        GeolabPortfolioDBContext _context;

        public AccountController()
        {
            _context = new GeolabPortfolioDBContext();
        }

        public ActionResult Login()
        {
            UserLoginViewModel vm = new UserLoginViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string hashed = PasswordHasher.GetHash(vm.Password);

            User loginedUser = _context.Users
                .Where(x => x.Email.ToString() == vm.Email && x.Password.ToString() == hashed)
                .FirstOrDefault();

            
            if (loginedUser == null) {
                ModelState.AddModelError("Errors", "მომხარებელის პაროლი ან ელ.ფოსტა არასწორია");
                return View(vm);
            }

            Session["Login-User"] = loginedUser;
            return RedirectToAction("Index", "Tags");
        }

        [AdminLoginFilter]
        public ActionResult Logout()
        {
            Session["Login-User"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        public ActionResult Test()
        {
            return View(_context.Tags.ToList());
        }
    }
}