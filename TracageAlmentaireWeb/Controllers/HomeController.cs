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

            FoodTrackerDao<EntiteProduit> dao = new FoodTrackerDao<EntiteProduit>("produits");
            EntiteProduit p = new EntiteProduit("bob le poireau","salement défoncé");
            dao.Add(p);
            p = dao.GetByIdentifier("bob le poireau","Nom");
            EntiteProduit p2 = new EntiteProduit("bob le poireau", "il est dech");
            dao.Update(p2,p.Id);
            return View();
        }
    }
}
