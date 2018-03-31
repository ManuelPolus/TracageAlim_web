using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            #region lol je teste mes accès

            FoodTrackerDao<EntiteRole> dao = new FoodTrackerDao<EntiteRole>();
            List<EntiteRole> plist = new List<EntiteRole>();
            EntiteRole role = dao.GetByIdentifier("il administre", "DescriptionRole");

            FoodTrackerDao<EntiteEtat> dao2 = new FoodTrackerDao<EntiteEtat>();
            EntiteEtat ee = dao2.GetByIdentifier("2");

            #endregion


            //plist=  dao.All().ToList();

            return View();
        }
    }
}
