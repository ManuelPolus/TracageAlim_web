using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    [AutoValidateAntiforgeryToken]
    public class TreatmentsManagementController : Controller, IControlManagement
    {
        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Treatment> tList = new List<Treatment>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                tList = mapper.GetTreatments();
                return View(tList);
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
        public ActionResult Create(Treatment t,State s)
        {
            t.OutgoingState = s;
            mapper.CreateTreatment(t);
            return RedirectToAction("List");

        }

        
        public ActionResult Update(long id) // Normalement id passé automatiquement à la vue.
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Treatment t = mapper.GetTreatment(id);
                return View(t);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Update(long id, Treatment t)
        {
            mapper.UpdateTreatment(id,t);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var t = mapper.GetTreatment(id);
                return View(t);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult Delete(long id)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteTreatment(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");
        }
    }
}