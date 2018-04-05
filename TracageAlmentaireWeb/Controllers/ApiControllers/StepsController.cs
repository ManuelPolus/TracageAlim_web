using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class StepsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");

        [Route("api/Steps")]
        public IEnumerable<Step> Get()
        {
            return mapper.GetSteps();
        }

        [Route("api/Steps/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetStep(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }


        public void Post(Step data)
        {
            mapper.CreateStep(data);
        }

        public void Put(long id, Step data)
        {
           mapper.UpdateStep(id,data);
        }

        public void Delete(long id)
        {
           mapper.DeleteStep(id);
        }
    }

}