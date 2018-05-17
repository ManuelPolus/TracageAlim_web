using System.Web.Mvc;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ManagementController
{
    public class UserScanRightsManagementController : Controller
    {
        Mapper mapper = new Mapper("FTDb");

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(long treatmentId, long roleId)
        {
            mapper.CreateUserScanRights(roleId,treatmentId);
        }
    }
}