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
            IEnumerable<EntiteProduit> list = new List<EntiteProduit>();
            FoodTrackerDao<EntiteProduit> dao = new FoodTrackerDao<EntiteProduit>("produits");
            list =  dao.Get();
            return View(list);
        }
    }
}
