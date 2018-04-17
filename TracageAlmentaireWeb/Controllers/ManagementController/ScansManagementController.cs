using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{

    [AutoValidateAntiforgeryToken]
    public class ScansManagementController : Controller
    {
        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Scan> sList = new List<Scan>();

            sList = mapper.GetScans();
            return View(sList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(Scan s)
        {
            mapper.CreateScan(s);
            return RedirectToAction("List");

        }

        public ActionResult Details(long idUser, long idTreatment)
        {
            var s = mapper.GetScan(idUser, idTreatment);
            return View(s);
        }

        public ActionResult Delete(long idUser, long idTreatment)
        {
            mapper.DeleteScan(idUser,idTreatment);
            return RedirectToAction("List");
        }
    }
}