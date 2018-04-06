using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Components;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

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



        public ActionResult LogInPage()
        {
          
            return View();
        }

        public ActionResult logIn()
        {

            return View("Index");
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
