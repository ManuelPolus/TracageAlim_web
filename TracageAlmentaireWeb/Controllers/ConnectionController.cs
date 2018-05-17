using System.Web.Mvc;
using System.Web.Security;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.ViewModels;

namespace TracageAlmentaireWeb.Controllers
{
    public class ConnectionController : Controller
    {

        private Mapper mapper = new Mapper("FTDb");

        public ActionResult LogInPage()
        {
  
            return View();
        }

        [HttpPost]
        public ActionResult logIn(AuthenticationViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User u = mapper.AthentifyUser(vm);
                if (u != null)
                {
                    FormsAuthentication.Initialize();
                    FormsAuthentication.SetAuthCookie(u.Id.ToString(), false);

                    return RedirectToAction("Index", "Home", u);
                }

                ModelState.AddModelError("WrongLogin", "Nom ou mot de passe incorrect");
            }

            return RedirectToAction("LoginPage");
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogInPage");
        }


    }
}