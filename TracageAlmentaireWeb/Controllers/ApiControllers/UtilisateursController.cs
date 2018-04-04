using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class UtilisateursController : ApiController
    {

        private Mapper mapper = new Mapper("FTDb");

        [Route("api/Utilisateurs")]
        public IEnumerable<Utilisateur> Get()
        {
            return mapper.GetUsers();
        }

        [Route("api/Utilisateurs/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetUser(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(Utilisateur data)
        {
            mapper.CreateUser(data);
        }

        public void Put(long id, Utilisateur data)
        {
            mapper.UpdateUser(id, data);
        }

        public void Delete(long id)
        {
            mapper.DeleteUser(id);
        }

    }
}