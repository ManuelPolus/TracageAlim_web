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
    public class SubContractorsManagementController : Controller, IControlManagement
    {

        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<SubContractor> subList = new List<SubContractor>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                subList = mapper.GetSubContractors();
                return View(subList);
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
        public ActionResult Create(SubContractor subContractor)
        {
            mapper.CreateSubContractor(subContractor);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                SubContractor sc = mapper.GetSubContractor(id);
                return View(sc);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, SubContractor subContractor)
        {
            mapper.UpdateSubContractor(id, subContractor);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var subContractor = mapper.GetSubContractor(id);
                return View(subContractor);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult Delete(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteSubContractor(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");
        }
    }
}