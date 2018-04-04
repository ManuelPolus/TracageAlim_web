using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class EtatsController : ApiController
    {

        FoodTrackerDao<EntiteEtat> dao = new FoodTrackerDao<EntiteEtat>("etats");

        [Route("api/Etats")]
        public IEnumerable<EntiteEtat> Get()
        {
            return dao.Get();
        }

        [Route("api/Etats/{identifier}")]
        public IHttpActionResult Get(string identifier)
        {
            var result = dao.GetByIdentifier(identifier);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("api/Etats/{identifier}/{identifierName}")]
        public IHttpActionResult Get(string identifier, string identifierName)
        {
            var result = dao.GetByIdentifier(identifier, identifierName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        public void Post(EntiteEtat data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteEtat data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}