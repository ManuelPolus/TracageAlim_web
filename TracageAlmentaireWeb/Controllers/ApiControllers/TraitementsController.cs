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
        public IHttpActionResult Get(string identifier)
        {
            var result = dao.GetByIdentifier(identifier);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("api/Traitments/{identifier}/{identifierName}")]
        public IHttpActionResult Get(string identifier, string identifierName)
        {
            var result = dao.GetByIdentifier(identifier, identifierName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
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