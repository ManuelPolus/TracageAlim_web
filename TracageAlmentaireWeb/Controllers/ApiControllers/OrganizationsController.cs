using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class OrganizationsController : ApiController
    {

        Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [Route("api/Organizations")]
        public IHttpActionResult Get()
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                IEnumerable<Address> ad = mapper.GetAddresses();

                if (ad != null)
                {
                    return Ok(mapper.GetOrganizations());
                }
                else
                {
                    return NotFound();
                }
            }
            return new ForbiddenActionResult(Request, "access denied");

        }

        [Route("api/Organizations/{id}")]
        public IHttpActionResult Get(long id)
        {

            IHttpActionResult reponse;

            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetOrganization(id);
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

        public void Post(Organization data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateOrganization(data);
            }
        }

        public void Put(long id, Organization data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateOrganization(id, data);
            }
        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteOrganization(id);
            }
        }

    }
}