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
    public class OrganizationsController : ApiController
    {

        Mapper mapper = new Mapper("FTDb");

        [Route("api/Organizations")]
        public IEnumerable<Organization> Get()
        {
            return mapper.GetOrganizations();
        }

        [Route("api/Organizations/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetOrganization(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(Organization data)
        {
            mapper.CreateOrganization(data);
        }

        public void Put(long id, Organization data)
        {
            mapper.UpdateOrganization(id,data);
        }

        public void Delete(long id)
        {
            mapper.DeleteOrganization(id);
        }

    }
}