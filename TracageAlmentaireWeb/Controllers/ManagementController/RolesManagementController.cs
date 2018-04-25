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
    public class RolesManagementController : Controller
    {
        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Role> rList = new List<Role>();

            rList = mapper.GetRoles();
            return View(rList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(Role r)
        {
            mapper.CreateRole(r);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            Role r = mapper.GetRole(id);
            return View(r);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, Role r)
        {
            mapper.UpdateRole(id, r);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            var r = mapper.GetRole(id);
            return View(r);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteRole(id);
            return RedirectToAction("List");
        }
    }
}