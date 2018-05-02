using AlimBlockChain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Windows.Forms;
using AlimBlockChain.BlocksAndUtilities;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers
{

    //TODO:  créer un process en db pour le testing.
    public class HomeController : Controller
    {
        private User user = new User { Name = "bob", Email = "b@b.com", Password = "abcd1234", CurrentRole_Id = 1,Telephone = "12345"};
        private Mapper mapper = new Mapper("FTDb");
        
        public ActionResult Index(User u)
        {
            try
            {
                u = mapper.GetUser(long.Parse(HttpContext.User.Identity.Name));


                #region TestBlockchain du cul

                Dictionary<string,string> data = new Dictionary<string, string>();
                Dictionary<string, string> data2 = new Dictionary<string, string>();
                data.Add("product","nom du produit");
                data.Add("Subcontractor","AU bon beurre");
                data.Add("Robin"," il est présent");
                data2.Add("pouetpouet","block invalide");



                BlockChain blockchain = new BlockChain(4);


                blockchain.AddBlock(blockchain.NewBlock(data));
                blockchain.AddBlock(blockchain.NewBlock(data));
                blockchain.AddBlock(blockchain.NewBlock(data));

                ValidityGranter vg = new ValidityGranter(blockchain);
                System.Diagnostics.Debug.WriteLine("Blockchain valid ? " + vg.IsBlockChainValid());
                FileDistributor fd = new FileDistributor();

                #endregion


                return View(u);
            }
            catch (System.FormatException fex)
            {
                return RedirectToAction("LoginPage", "Connection");
            }
            
           
            
        }

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

        public ActionResult QrGeneration(string productName, int batchSize)
        {
            return View("Index");
        }

        //TODO: déplacer vers la BL

    }
}
