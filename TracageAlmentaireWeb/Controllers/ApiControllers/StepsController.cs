using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class StepsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [Route("api/Steps")]
        public IHttpActionResult Get()
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                return Ok(mapper.GetSteps());
            }
            else
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
        }

        [Route("api/Steps/{id}")]
        public IHttpActionResult Get(long id)
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetStep(id);
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


        public void Post(Step data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateStep(data);
            }
                
        }

        public void Put(long id, Step data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateStep(id, data);
            }
                
        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteStep(id);
            }
                
        }
    }

}