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
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                oList = mapper.GetOrganizations();
                return View(oList);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult Create()
        {

            List<Organization> oList = new List<Organization>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                return View();
            }
            return RedirectToAction("LoginPage", "Connection");
           
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
            Organization o = mapper.GetOrganization(id);
            return View(o);
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

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var o = mapper.GetOrganization(id);
                return View(o);
            }
            return RedirectToAction("LoginPage", "Connection");
            
        }

        public ActionResult Delete(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteOrganization(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");
        }
    }
}