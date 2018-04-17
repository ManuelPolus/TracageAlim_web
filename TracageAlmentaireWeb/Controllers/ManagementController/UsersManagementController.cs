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
    public class UsersManagementController : Controller, IControlManagement
    {
        // GET: UsersManagement

        Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<User> uList = new List<User>();
            uList = mapper.GetUsers();

            return View(uList);
        }

        public ActionResult Create()
        {

            return View();
        }
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(User u)
        {
            mapper.CreateUser(u);
            return RedirectToAction("List");
        }

        public ActionResult Update(long id)
        {
            return View();
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
            User u = mapper.GetUser(id);
            return View(u);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteUser(id);
            return RedirectToAction("List");
        }
    }
}