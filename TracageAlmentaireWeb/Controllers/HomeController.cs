﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.Helpers;
using System.Web.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Components.PDF;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace TracageAlmentaireWeb.Controllers
{

    //TODO:  créer un process en db pour le testing.
    public class HomeController : Controller
    {
        private User user = new User { Name = "bob", Email = "b@b.com", Password = "abcd1234", CurrentRole_Id = 1 };
        private Mapper mapper = new Mapper("FTDb");

        [AllowAnonymous]
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";
            /*
            mapper.CreateRole(new Role { Name = "Default", Description = "This role is provided by default to new Users." });

            mapper.CreateUser(user);

            mapper.CreateProduct(new Product { Name = "Banane", Description = "for the scale", QRCode = "banana001", ProcessId = 1 });
            Treatment t = new Treatment { Name = "gathering", Position = 1, Description = "gathering of the bananas", OutgoingState = new State { Status = "gathered banana" } };
            Treatment t2 = new Treatment { Name = "stocking", Position = 2, Description = "we stock the bananas", OutgoingState = new State { Status = "stocked banana" } };
            List<Treatment> firstStep = new List<Treatment> { t, t2 };

            Treatment t3 = new Treatment { Name = "transport", Position = 1, Description = "moving the bananas", OutgoingState = new State { Status = "stocked banana" } };
            List<Treatment> secondStep = new List<Treatment> { t3 };

            Treatment t4 = new Treatment { Name = "killing", Position = 1, Description = "murder of the bananas", OutgoingState = new State { Status = "dead banana" } };
            Treatment t5 = new Treatment { Name = "peeling", Position = 2, Description = "peeling of the bananas", OutgoingState = new State { Status = "peeled banana" } };
            List<Treatment> tirdStep = new List<Treatment> { t4, t5 };


            Step s = new Step { Name = "bananaFarm", Position = 1, Treatments = firstStep, Process_Id = 1 };

            Step s2 = new Step { Name = "bananaTruckTransport", Position = 2, Treatments = secondStep, Process_Id = 1 };

            Step s3 = new Step { Name = "bananabattoir", Position = 3, Treatments = tirdStep, Process_Id = 1 };


            mapper.CreateProcess(new Process { Name = "the banana process", Description = "process for the scale and it is a peeling ahahahahhah", Steps = new List<Step> { s, s2, s3 } });

            mapper.CreateAddress(new Address { Number = "42", Street = "DefaultStreet", Country = "DefaultLand", PostalCode = "0000" });
            */
            return View();
        }


        //[HttpGet]
        //public ActionResult Index(User u)
        //{

        //    return View();
        //}

        public ActionResult ProcessCreationPage()
        {
            return View();
        }


        public ActionResult StepCreationPage(int num)
        {
            ViewBag.NumberOfSteps = num;
            return View();
        }

        public ActionResult TreatementCreationPage()
        {
            return PartialView();
        }

        public ActionResult QRGenerationPage()
        {
            return View();
        }

        public ActionResult QrGeneration(string productName,int batchSize)
        {
            
            return View("Index");
        }

        //TODO: déplacer vers la BL
        
    }
}
