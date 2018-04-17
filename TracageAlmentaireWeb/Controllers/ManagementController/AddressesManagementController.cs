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

        public ActionResult List()
        {
            List<Address> aList = new List<Address>();

            aList = mapper.GetAddresses();
            return View(aList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(Address a)
        {
            mapper.CreateAddress(a);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, Address a)
        {
            mapper.UpdateAddress(id, a);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            var a = mapper.GetAddress(id);
            return View(a);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteAddress(id);
            return RedirectToAction("List");
        }
    }
}