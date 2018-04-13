using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.ViewModels;

namespace TracageAlmentaireWeb.Controllers
{
    public class ConnectionController : Controller
    {

        private Mapper mapper = new Mapper("FTDb");

        public ActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel userToRegister)
        {
            if (ModelState.IsValid)
            {
                User u = new User();

                u.Name = userToRegister.FullName;
                u.Email = userToRegister.Email;
                u.Password = userToRegister.Password;
                u.Telephone = userToRegister.PhoneNumber;
                u.Address = new Address(
                        userToRegister.StreetName,
                        userToRegister.Number,
                        userToRegister.PostalCode,
                        userToRegister.Country
                    );
                if (userToRegister.CheckPasswords())
                {
                    mapper.CreateUser(u);
                }

                return View("logInPage");
            }

            return View();
        }

        public ActionResult LogInPage() => View();

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