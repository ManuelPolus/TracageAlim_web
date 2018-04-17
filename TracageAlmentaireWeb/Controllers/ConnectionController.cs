using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Tracage.Models;
using TracageAlmentaireWeb.BL;
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
        public ActionResult logIn(AuthenticationViewModel vm)
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

            return Redirect("/home/Index");
        }


    }
}