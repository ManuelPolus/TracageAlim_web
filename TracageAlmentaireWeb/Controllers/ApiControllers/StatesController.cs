using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class StatesController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");

        [Route("api/States")]
        public IEnumerable<State> Get()
        {
            return mapper.GetStates();
        }

        [Route("api/States/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetState(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }


        public void Post(State data)
        {
            mapper.CreateState(data);
        }

        public void Put(long id, State data)
        {
            mapper.UpdateState(id, data);
        }

        public void Delete(long id)
        {
            mapper.DeleteState(id);
        }

    }
}