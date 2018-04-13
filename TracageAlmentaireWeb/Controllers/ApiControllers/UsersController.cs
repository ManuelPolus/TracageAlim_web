using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    //TODO: replacer pa un viewmodel pour ne pas montrer le salt
    public class UsersController : ApiController
    {

        private Mapper mapper = new Mapper("FTDb");

        [Route("api/Users")]
        public IEnumerable<User> Get()
        {
            return mapper.GetUsers();
        }



        [Route("api/Users/{email}")]
        public IHttpActionResult Get(string email)
        {
            var result = mapper.GetUser(email);
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