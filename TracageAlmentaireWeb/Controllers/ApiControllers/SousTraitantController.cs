using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class SousTraitantController : ApiController
    {

        FoodTrackerDao<EntiteSousTraitant> dao = new FoodTrackerDao<EntiteSousTraitant>("SousTraitant");

        [Route("api/SousTraitants")]
        public IEnumerable<EntiteSousTraitant> Get()
        {
            return dao.Get();
        }

        [Route("api/SousTraitan/{identifier}")]
        public EntiteSousTraitant Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteSousTraitant data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteSousTraitant data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}