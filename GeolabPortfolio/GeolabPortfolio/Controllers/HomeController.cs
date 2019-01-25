using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public String Test()
        {
            return PasswordHasher.GetHash("123456789");
        }
    }
}