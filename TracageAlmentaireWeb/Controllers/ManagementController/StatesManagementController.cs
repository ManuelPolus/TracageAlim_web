using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{

    [AutoValidateAntiforgeryToken]
    public class StatesManagementController : Controller
    {
        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<State> sList = new List<State>();

            sList = mapper.GetStates();
            return View(sList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(State s)
        {
            mapper.CreateState(s);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            State st = mapper.GetState(id);
            return View(st);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, State s)
        {
            mapper.UpdateState(id, s);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            var s = mapper.GetState(id);
            return View(s);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteState(id);
            return RedirectToAction("List");
        }
    }
}