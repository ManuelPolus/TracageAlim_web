using System.Collections.Generic;
using System.Web.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    public class ProcessesManagementController : Controller
    {
        Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Process> pList = new List<Process>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                pList = mapper.GetProcesses();
                return View(pList);
            }
            return RedirectToAction("LoginPage", "Connection");
        }

        public ActionResult PartialList()
        {
           
            return PartialView();

        }

        public ActionResult GoToStepDetails(long stepId)
        {
            return RedirectToAction("Details", "StepsManagement", stepId);
        }


        public ActionResult Create()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("LoginPage", "Connection");
        }

        [HttpPost]
        public ActionResult Create(Process p)
        {
            mapper.CreateProcess(p);
            return RedirectToAction("Create","StepsManagement",p);
        }

        public ActionResult Update(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Process p = mapper.GetProcess(id);
                return View(p);
            }

            return RedirectToAction("LoginPage", "Connection");
        }

        [HttpPost]
        public ActionResult Update(long id, Process p)
        {
            mapper.UpdateProcess(id,p);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Process p = mapper.GetProcess(id);
                return View(p);
            }

            return RedirectToAction("LoginPage", "Connection");
           
        }

        public ActionResult Delete(long id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                mapper.DeleteProcess(id);
                return RedirectToAction("List");
            }

            return RedirectToAction("LoginPage", "Connection");
            
        }
    }
}