using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class ProcessesController : ApiController
    {

        Mapper mapper = new Mapper("FTDb");

        [Route("api/Processes")]
        public IEnumerable<Process> Get()
        {
            return mapper.GetProcesses();
        }

        [Route("api/Processes/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetProcess(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }


        public void Post(Process data)
        {
            mapper.CreateProcess(data);
        }

        public void Put(long id, Process data)
        {
            mapper.UpdateProcess(id, data);
        }

        public void Delete(long id)
        {
            mapper.DeleteProcess(id);
        }

    }
}