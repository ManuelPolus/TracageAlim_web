using System.Web.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers
{

    //TODO:  créer un process en db pour le testing.
    public class HomeController : Controller
    {
        private User user = new User { Name = "bob", Email = "b@b.com", Password = "abcd1234", CurrentRole_Id = 1,Telephone = "12345"};
        private Mapper mapper = new Mapper("FTDb");
        
        public ActionResult Index(User u)
        {
            try
            {
                u = mapper.GetUser(long.Parse(HttpContext.User.Identity.Name));
                return View(u);
            }
            catch (System.FormatException fex)
            {
                return RedirectToAction("LoginPage", "Connection");
            }
            
           
            
        }

        public ActionResult ProcessCreationPage()
        {
            return View();
        }


        public ActionResult StepCreationPage(int num)
        {
            ViewBag.NumberOfSteps = num;
            return View();
        }

        public ActionResult TreatementCreationPage()
        {
            return PartialView();
        }

        public ActionResult QRGenerationPage()
        {
            return View();
        }

        public ActionResult QrGeneration(string productName, int batchSize)
        {
            return View("Index");
        }

        //TODO: déplacer vers la BL

    }
}
