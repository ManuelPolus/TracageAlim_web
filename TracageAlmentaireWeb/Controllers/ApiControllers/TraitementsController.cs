using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class TraitementsController : ApiController
    {
        FoodTrackerDao<EntiteTraitement> dao = new FoodTrackerDao<EntiteTraitement>("Traitements");

        [Route("api/Traitements")]
        public IEnumerable<EntiteTraitement> Get()
        {
            return dao.Get();
        }

        [Route ("api/Traitements/{identifier}")]
        public EntiteTraitement Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteTraitement data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteTraitement data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }
    }
}