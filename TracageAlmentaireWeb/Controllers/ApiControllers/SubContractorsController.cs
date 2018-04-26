using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class SubContractorsController : ApiController
    {

        Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [Route("api/SubContractors")]
        public IHttpActionResult Get()
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                return Ok(mapper.GetSubContractors());
            }
            else
            {
                return new ForbiddenActionResult(Request, "access denied");
            }

        }

        [Route("api/SubContractors/{id}")]
        public IHttpActionResult Get(long id)
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetSubContractor(id);
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



        public void Post(SubContractor data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateSubContractor(data);
            }

        }

        public void Put(long id, SubContractor data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateSubContractor(id, data);
            }
        }


        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteSubContractor(id);
            }
                
        }

    }
}