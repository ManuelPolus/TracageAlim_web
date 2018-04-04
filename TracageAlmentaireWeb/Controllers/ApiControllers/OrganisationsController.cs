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

        [Route("api/Organisations/{identifier}")]
        public IHttpActionResult Get(string identifier)
        {
            var result = dao.GetByIdentifier(identifier);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("api/Organisations/{identifier}/{identifierName}")]
        public IHttpActionResult Get(string identifier, string identifierName)
        {
            var result = dao.GetByIdentifier(identifier, identifierName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
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