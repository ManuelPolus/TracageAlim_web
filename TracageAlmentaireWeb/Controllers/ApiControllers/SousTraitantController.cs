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

        FoodTrackerDao<EntiteSousTraitant> dao = new FoodTrackerDao<EntiteSousTraitant>("SubContractors");

        [Route("api/SubContractors")]
        public IEnumerable<EntiteSousTraitant> Get()
        {
            return dao.Get();
        }

        [Route("api/SubContractors/{identifier}")]
        public IHttpActionResult Get(string identifier)
        {
            var result = dao.GetByIdentifier(identifier);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("api/SubContractors/{identifier}/{identifierName}")]
        public IHttpActionResult Get(string identifier, string identifierName)
        {
            var result = dao.GetByIdentifier(identifier, identifierName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
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