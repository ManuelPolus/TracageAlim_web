using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TracageAlmentaireWeb.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ProductManagementController : Controller, IControlManagement
    {
        Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Product> plist = new List<Product>();
            plist = mapper.GetProducts();
            return View(plist);
        }



        public ActionResult Create()
        { 
            return View();
        }
        [System.Web.Mvc.HttpPost]
        [IgnoreAntiforgeryToken]
        public ActionResult Create(Product p)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    mapper.CreateProduct(p);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            return RedirectToAction("List");
        }



        public ActionResult Update(long id)
        {
            return View();
        }
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Update(long id, Product p)
        {
            if (ModelState.IsValid)
            {
                mapper.UpdateProduct(id, p);
            }

            return RedirectToAction("List");
        }



        public ActionResult Details(long id)
        {
            Product p = mapper.GetProductById(id);
            return View(p);
        }

        public ActionResult Delete(long id)
        {
            try
            {
                mapper.DeleteProduct(id);
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                return View();
            }
            
        }


    }
}