using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Tracage.Models;
using TracageAlmentaireWeb.Controllers.ApiControllers;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;


namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    public class TreatmentsManagementController : Controller
    {
        private Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Treatment> tList = new List<Treatment>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                tList = mapper.GetTreatments();
                return View(tList);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        
        public ActionResult Create(Process p)
        {
            p = mapper.GetProcess(p.Id);
            TempData["process"] = p;
            var s = TempData["SelectedStep"];
            if (s == null)
            {
                TempData["SelectedStep"] = p.Steps.ElementAt(0);
            }
            else
            {
                TempData["SelectedStep"]= s;
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("LoginPage", "Connection");
        }



        [HttpPost]
        public ActionResult Create(Treatment t,State s)
        {
            t.OutgoingState = s;
            mapper.CreateTreatment(t);//            <-
            Step step = mapper.GetStep(t.StepId);
            TempData["SelectedStep"] = step;
            Process p = mapper.GetProcess(step.Process_Id);
            TempData["process"] = p;
            if (t.OutgoingState.Final)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Create",p);
        }
        

        
        public ActionResult Update(long id) // Normalement id passé automatiquement à la vue.
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Treatment t = mapper.GetTreatment(id);
                return View(t);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Update(long id, Treatment t)
        {
            mapper.UpdateTreatment(id,t);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var t = mapper.GetTreatment(id);
                return View(t);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult Delete(long id)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteTreatment(id);
                return RedirectToAction("List");
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult ChangeStep(Step st)
        {
            
            Process p = mapper.GetProcess(st.Process_Id);
            TempData["SelectedStep"] = st;
            return RedirectToAction("Create",p);
        }
    }
}