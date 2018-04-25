using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Components;
using TracageAlmentaireWeb.BL.Components.PDF;
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                plist = mapper.GetProducts();
                return View(plist);
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
        [IgnoreAntiforgeryToken]
        public ActionResult Create(Product p)
        {

            if (ModelState.IsValid)
            {
                try
                {
                   
                   
                    p.QRCode = QrGenerator.GenerateQRCodeString(p);
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

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Product p = mapper.GetProductById(id);
                return View(p);
            }
            return RedirectToAction("LoginPage", "Connection");
            
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Product p = mapper.GetProductById(id);
                return View(p);
            }
            return RedirectToAction("LoginPage", "Connection");

        }

        public ActionResult Delete(long id)
        {
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    mapper.DeleteProduct(id);
                    return RedirectToAction("List");
                }
                return RedirectToAction("LoginPage", "Connection");
            }
            catch (Exception e)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return View();
                }
                return RedirectToAction("LoginPage", "Connection");
            }
            
        }

        public ActionResult GeneratePdfWithPRoductInformation(Product p)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                PdfQRWriter pw = new PdfQRWriter();
                pw.CreateOrRefreshDocument();
                pw.AddInfo(p);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");

        }


    }
}