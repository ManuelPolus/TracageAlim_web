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

                subList = mapper.GetSubContractors();
                return View(subList);
            }

            public ActionResult Create()
            {
                return View();
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
                return View();
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
                var subContractor = mapper.GetSubContractor(id);
                return View(subContractor);
            }

            public ActionResult Delete(long id)
            {
                mapper.DeleteSubContractor(id);
                return RedirectToAction("List");
            }
        }
}