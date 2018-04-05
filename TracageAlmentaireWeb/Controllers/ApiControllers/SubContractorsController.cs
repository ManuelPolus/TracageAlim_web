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
    public class SubContractorsController : ApiController
    {

        Mapper mapper = new Mapper("FTDb");

        [Route("api/SubContractors")]
        public IEnumerable<SubContractor> Get()
        {
            return mapper.GetSubContractors();
        }

        [Route("api/SubContractors/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetSubContractor(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

       

        public void Post(SubContractor data)
        {
            mapper.CreateSubContractor(data);
        }

        public void Put(long id, SubContractor data)
        {
           mapper.UpdateSubContractor(id,data);
        }

        public void Delete(long id)
        {
            mapper.DeleteSubContractor(id);
        }

    }
}