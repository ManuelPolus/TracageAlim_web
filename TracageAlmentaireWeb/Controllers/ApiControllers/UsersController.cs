using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
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

        [Route("api/users/{mail}")]
        public IHttpActionResult Get(string mail)
        {
            var result = mapper.GetUser(mail);
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