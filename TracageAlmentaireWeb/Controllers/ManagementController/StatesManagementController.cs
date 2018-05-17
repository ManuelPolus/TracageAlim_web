using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                sList = mapper.GetStates();
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
        public ActionResult Create(State s)
        {
            mapper.CreateState(s);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                State st = mapper.GetState(id);
                return View(st);
            }
            return RedirectToAction("LoginPage", "Connection");
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var s = mapper.GetState(id);
                return View(s);
            }
            return RedirectToAction("LoginPage", "Connection");

        }

        public ActionResult Delete(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteState(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");
        }
    }
}