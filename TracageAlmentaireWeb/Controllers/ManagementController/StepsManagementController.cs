using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Windows.Forms;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    [AutoValidateAntiforgeryToken]
    public class StepsManagementController : Controller
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

        public ActionResult Create(Process p)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                p = mapper.GetProcess(p.Id);
                ViewBag.Process = p;
                TempData["Process"] = p;
                return View();
            }
            return RedirectToAction("LoginPage", "Connection");

        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(Step s)
        {

            mapper.CreateStep(s);
            Process p = mapper.GetProcess(s.Process_Id);
            return RedirectToAction("Create", p);

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

        public ActionResult GotoTreatmentsCreation()
        {
            Process p = (Process)TempData["Process"];
            p = mapper.GetProcess(p.Id);
            Step s = p.Steps.ElementAt(0);
            return RedirectToAction("Create", "TreatmentsManagement",p);
        }
    }
}