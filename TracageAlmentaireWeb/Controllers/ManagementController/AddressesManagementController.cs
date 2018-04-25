using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    [AutoValidateAntiforgeryToken]
    public class AddressesManagementController : Controller
    {
        private Mapper mapper = new Mapper("FTDb");


        public ActionResult Create()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("LoginPage", "Connection");
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public void Create(Address a)
        {
            mapper.CreateAddress(a);
        }


       
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public void Update(long id, Address a)
        {
            mapper.UpdateAddress(id, a);
        }


        public void Delete(long id)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteAddress(id);
            }
            //return RedirectToAction("LoginPage", "Connection");
        }
    }
}