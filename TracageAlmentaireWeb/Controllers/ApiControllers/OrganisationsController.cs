using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class OrganisationsController : ApiController
    {

        FoodTrackerDao<EntiteOrganisation> dao = new FoodTrackerDao<EntiteOrganisation>("organisations");

        [Route("api/Organisations")]
        public IEnumerable<EntiteOrganisation> Get()
        {
            return dao.Get();
        }

        [Route("api/Organisation/{identifier}")]
        public EntiteOrganisation Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteOrganisation data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteOrganisation data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}