using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            User bob = new User { Name = "bob", Email = "b@b.cul",Password = "abcd1234",CurrentRole = new Role{Name = "adminbob",Description = "being fckn bob"}};
            Mapper m = new Mapper("FTDb");
            m.CreateUser(bob);
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CreateAdresse()
        {
            return View();
        }

        public ActionResult AllProducts()
        {
            IEnumerable<User> list = new List<User>();
            Mapper mapper = new Mapper("FTDb");
            list = mapper.GetUsers();
            //FoodTrackerDao<EntiteProduit> dao = new FoodTrackerDao<EntiteProduit>("produits");
            //list =  dao.Get();
            return View(list);
        }
    }
}
