using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Components;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.ViewModels;

namespace TracageAlmentaireWeb.Controllers
{
    public class HomeController : Controller
    {
        private User user = new User { Name = "bob", Email = "b@b.com", Password = "abcd1234", CurrentRole_Id = 1 };
        private Mapper mapper = new Mapper("FTDb");
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            User bob = mapper.GetUser(1);
            bool ok = PasswordHasher.CheckPassword("abcd1234" + bob.Salt, bob.Password);
            return View();
        }
        [HttpGet]
        public ActionResult Index(User u)
        {
           
            return View();
        }

        public ActionResult AllProducts()
        {
            IEnumerable<User> list = new List<User>();
            Mapper mapper = new Mapper("FTDb");
            list = mapper.GetUsers();

            return View(list);
        }
    }
}
