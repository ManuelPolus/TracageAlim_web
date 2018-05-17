using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    //TODO: replacer pa un viewmodel pour ne pas montrer le salt
    public class UsersController : ApiController
    {

        private Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [Route("api/Users")]
        public IHttpActionResult Get()
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                return Ok(mapper.GetUsers());
            }
            else
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
                
        }

        [Route("api/Users/{email}")]
        public IHttpActionResult Get(string email)
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetUser(email);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        public void Post(User data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateUser(data);
            }
                
        }

        public void Put(long id, User data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateUser(id, data);
            }
                
        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteUser(id);
            }
                
        }

    }
}