using System.Web.Mvc;

namespace TracageAlmentaireWeb.Controllers
{
    interface IControlManagement
    {

        ActionResult List();
        ActionResult Create();

        ActionResult Update(long id);
        ActionResult Details(long id);
        ActionResult Delete(long id);

    }
}
