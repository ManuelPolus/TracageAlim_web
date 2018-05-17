using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class RolesController : ApiController
    {

        Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();


        [Route("api/Roles")]
        public IHttpActionResult Get()
        {
           
            if (keyHandler.CheckApiKey(this.Request))
            {
                return Ok(mapper.GetRoles());
            }
            else
            {
                return new ForbiddenActionResult(Request, "access denied");
            }

        }

        [Route("api/Roles/{id}")]
        public IHttpActionResult Get(long id)
        {

            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetRole(id);
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


        public void Post(Role data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateRole(data);
            }
        }

        public void Put(long id, Role data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateRole(id, data);
            }
                
        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteRole(id);
            }
                
        }

    }
}