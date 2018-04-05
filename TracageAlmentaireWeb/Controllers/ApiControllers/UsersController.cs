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
    public class UsersController : ApiController
    {

        private Mapper mapper = new Mapper("FTDb");

        [Route("api/Users")]
        public IEnumerable<User> Get()
        {
            return mapper.GetUsers();
        }

        [Route("api/users/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetUser(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(User data)
        {
            mapper.CreateUser(data);
        }

        public void Put(long id, User data)
        {
            mapper.UpdateUser(id, data);
        }

        public void Delete(long id)
        {
            mapper.DeleteUser(id);
        }

    }
}