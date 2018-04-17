using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    public class TreatmentsManagementController : Controller, IControlManagement
    {
        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Treatment> tList = new List<Treatment>();

            tList = mapper.GetTreatments();
            return View(tList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Treatment t)
        {
            mapper.CreateTreatment(t);
            return RedirectToAction("List");

        }

        
        public ActionResult Update(long id) // Normalement id passé automatiquement à la vue.
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(long id, Treatment t)
        {
            mapper.UpdateTreatment(id,t);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            var t = mapper.GetTreatment(id);
            return View(t);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteTreatment(id);
            return RedirectToAction("List");
        }
    }
}