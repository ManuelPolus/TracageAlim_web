using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    [AutoValidateAntiforgeryToken]
    public class UsersManagementController : Controller, IControlManagement
    {
        // GET: UsersManagement

        Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<User> uList = new List<User>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                
                uList = mapper.GetUsers();
                return View(uList);
            }
            return RedirectToAction("LoginPage", "Connection");
        }
        [Authorize]
        public ActionResult Create()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("LoginPage", "Connection");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(User u, Address a)
        {
            u.Address = a;
            mapper.CreateUser(u);
            return RedirectToAction("List");
        }

        public ActionResult Update(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                User u = mapper.GetUser(id);
                return View(u);
            }

            return RedirectToAction("LoginPage", "Connection");
        }
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, User u)
        {
            mapper.UpdateUser(id,u);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                User u = mapper.GetUser(id);
                return View(u);
            }

            return RedirectToAction("LoginPage", "Connection");
            
        }

        public ActionResult Delete(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteUser(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");         
        }
    }
}