using System.Collections.Generic;
using System.Web.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers
{

    //TODO:  créer un process en db pour le testing.
    public class HomeController : Controller
    {
        private User user = new User { Name = "bob", Email = "b@b.com", Password = "abcd1234", CurrentRole_Id = 1 };
        private Mapper mapper = new Mapper("FTDb");

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            mapper.CreateProduct(new Product {Name = "Banane" , Description = "for the scale", QRCode = "banana001"});
            return View();
        }


        //[HttpGet]
        //public ActionResult Index(User u)
        //{
           
        //    return View();
        //}

        public ActionResult ProcessCreationPage()
        {
            return View();
        }

        public ActionResult StepCreationPage()
        {
            ViewBag.NumberOfTreatements = 1;
           
            return View();
        }

        public ActionResult TreatementCreationPage()
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
