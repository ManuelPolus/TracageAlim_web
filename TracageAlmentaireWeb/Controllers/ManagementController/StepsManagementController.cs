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
    public class StepsManagementController : Controller, IControlManagement
    {

        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Step> sList = new List<Step>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                sList = mapper.GetSteps();
                return View(sList);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

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
        public ActionResult Create(Step s)
        {
            mapper.CreateStep(s);
            return RedirectToAction("Create");

        }


        public ActionResult Update(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Step s = mapper.GetStep(id);
                return View(s);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, Step s)
        {
            mapper.UpdateStep(id, s);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var s = mapper.GetStep(id);
                return View(s);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult Delete(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteStep(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");

        }
    }
}