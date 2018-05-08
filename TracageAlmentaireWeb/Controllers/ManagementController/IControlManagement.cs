using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Forms;

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
