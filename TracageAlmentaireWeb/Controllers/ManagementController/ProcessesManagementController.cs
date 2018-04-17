using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    public class ProcessesManagementController : Controller, IControlManagement
    {
        Mapper mapper = new Mapper("FTDb");

        public ActionResult List()
        {
            List<Process> pList = new List<Process>();
            pList = mapper.GetProcesses();
            return View(pList);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Process p)
        {
            mapper.CreateProcess(p);
            return RedirectToAction("List");
        }

        public ActionResult Update(long id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(long id, Process p)
        {
            mapper.UpdateProcess(id,p);
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            Process p = mapper.GetProcess(id);
            return View(p);
        }

        public ActionResult Delete(long id)
        {
            mapper.DeleteProcess(id);
            return RedirectToAction("List");
        }
    }
}