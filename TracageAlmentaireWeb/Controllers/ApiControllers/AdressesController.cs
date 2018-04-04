using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class AdressesController : ApiController
    {

        FoodTrackerDao<EntiteAdresse> dao = new FoodTrackerDao<EntiteAdresse>("adresses");

        [Route("api/Adresses")]
        public IEnumerable<EntiteAdresse> Get()
        {
            return dao.Get();
        }

        [Route("api/Adresses/{identifier}")]
        public IHttpActionResult Get(string identifier)
        {
            var result = dao.GetByIdentifier(identifier);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("api/Adresses/{identifier}/{identifierName}")]
        public IHttpActionResult Get(string identifier, string identifierName)
        {
            var result = dao.GetByIdentifier(identifier, identifierName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(EntiteAdresse data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteAdresse data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}