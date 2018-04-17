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
    public class OrganizationsManagementController : Controller
    {

        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Organization> oList = new List<Organization>();

            oList = mapper.GetOrganizations();
            return View(oList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(Organization o)
        {
            mapper.CreateOrganization(o);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, Organization o)
        {
            mapper.UpdateOrganization(id, o);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            var o = mapper.GetOrganization(id);
            return View(o);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteOrganization(id);
            return RedirectToAction("List");
        }
    }
}