using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class TreatmentsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();


        [Route("api/Treatments")]
        public IHttpActionResult Get()
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                return Ok(mapper.GetTreatments());
            }
            else
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            
        }

        [Route("api/Treatments/{id}")]
        public IHttpActionResult Get(long id)
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetTreatment(id);
                result.OutgoingState = mapper.GetState(result.OutgoingStateId);
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

        public void Post(Treatment data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateTreatment(data);
            }
                
        }

        public void Put(long id, Treatment data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateTreatment(id, data);
            }
                
        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteTreatment(id);
            }
                
        }
    }
}