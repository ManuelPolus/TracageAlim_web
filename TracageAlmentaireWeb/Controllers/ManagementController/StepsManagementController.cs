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

            sList = mapper.GetSteps();
            return View(sList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(Step s)
        {
            mapper.CreateStep(s);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            return View();
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
            var s = mapper.GetStep(id);
            return View(s);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteStep(id);
            return RedirectToAction("List");
        }
    }
}