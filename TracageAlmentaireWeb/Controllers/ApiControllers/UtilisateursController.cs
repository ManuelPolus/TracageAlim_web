using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class UtilisateursController : ApiController
    {

        FoodTrackerDao<EntiteUtilisateur> dao = new FoodTrackerDao<EntiteUtilisateur>("Utilisateurs");

        [Route("api/Utilisateurs")]
        public IEnumerable<EntiteUtilisateur> Get()
        {
            return dao.Get();
        }

        [Route("api/Utilisateurs/{identifier}")]
        public IHttpActionResult Get(string identifier)
        {
            var result = dao.GetByIdentifier(identifier);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("api/Utilsateurs/{identifier}/{identifierName}")]
        public IHttpActionResult Get(string identifier, string identifierName)
        {
            var result = dao.GetByIdentifier(identifier, identifierName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(EntiteUtilisateur data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteUtilisateur data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}