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
    public class TraitementsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");


        [Route("api/Traitements")]
        public IEnumerable<Treatement> Get()
        {
            return mapper.GetTreatments();
        }

        [Route("api/Traitements/{identifier}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetTreatment(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(Treatement data)
        {
            mapper.CreateTreatment(data);
        }

        public void Put(long id, Treatement data)
        {
            mapper.UpdateTreatment(id, data);
        }

        public void Delete(long id)
        {
            mapper.DeleteTreatment(id);
        }
    }
}