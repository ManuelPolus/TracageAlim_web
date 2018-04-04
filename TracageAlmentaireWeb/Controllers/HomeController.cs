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

           //Utilisateur bob = new Utilisateur();
           // bob.Nom = "bob";
           // bob.Email = "bobobobobobo@boobboob.com";
           // bob.CurrentRole = new Role();
           // bob.MotDePasse = "abcd1234";
           // bob.Telephone = "12345890";

           Mapper mapper = new Mapper("FTDb");
           // mapper.CreateUser(bob);

            Utilisateur bob = mapper.GetUser(5);
            bob.Nom = "belle batte";
            bob.Email = "boblebob@ntm.cul";
            mapper.UpdateUser(bob.Id,bob);
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
            IEnumerable<Utilisateur> list = new List<Utilisateur>();
            Mapper mapper = new Mapper("FTDb");
            list = mapper.GetUsers();
            //FoodTrackerDao<EntiteProduit> dao = new FoodTrackerDao<EntiteProduit>("produits");
            //list =  dao.Get();
            return View(list);
        }
    }
}
