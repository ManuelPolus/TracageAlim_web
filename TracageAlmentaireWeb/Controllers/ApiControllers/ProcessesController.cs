using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class ProcessesController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [Route("api/Processs")]
        public IHttpActionResult Get()
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            return Ok(mapper.GetProcesses());
        }

        [Route("api/Processs/{id}")]

        public IHttpActionResult Get(long id)
        {
            IHttpActionResult reponse;

            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }

            else
            {
                var result = mapper.GetProcess(id);
                if (result != null)
                {
                    reponse = Ok(result);
                }
                else
                {
                    reponse = NotFound();
                }
            }
            return reponse;
        }


        public void Post(Process data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateProcess(data);
            }
        }

        public void Put(long id, Process data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateProcess(id, data);
            }
        }


        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteProcess(id);
            }
        }

    }
}