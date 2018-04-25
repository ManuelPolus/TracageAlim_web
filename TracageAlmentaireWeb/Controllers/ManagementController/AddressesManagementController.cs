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
    public class AddressesManagementController : Controller
    {
        private Mapper mapper = new Mapper("FTDb");


        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public void Create(Address a)
        {
            mapper.CreateAddress(a);
        }


       
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public void Update(long id, Address a)
        {
            mapper.UpdateAddress(id, a);
        }


        public void Delete(long id)
        {
            mapper.DeleteAddress(id);
        }
    }
}