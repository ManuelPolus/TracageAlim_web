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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                rList = mapper.GetRoles();
                return View(rList);
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
        public ActionResult Create(Role r)
        {
            mapper.CreateRole(r);
            return RedirectToAction("List");

        }


        public ActionResult Update(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Role r = mapper.GetRole(id);
                return View(r);
            }
            return RedirectToAction("LoginPage", "Connection");
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var r = mapper.GetRole(id);
                return View(r);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult Delete(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteRole(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");

        }
    }
}