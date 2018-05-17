using System.Web.Http;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class StatesController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [Route("api/States")]
        public IHttpActionResult Get()
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                return Ok(mapper.GetStates());
            }
            else
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
        }

        [Route("api/States/{id}")]


        public IHttpActionResult Get(long id)
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetState(id);
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


        public void Post(State data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateState(data);
            }
                
        }

        public void Put(long id, State data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateState(id, data);
            }
                
        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteState(id);
            }
               
        }

    }
}