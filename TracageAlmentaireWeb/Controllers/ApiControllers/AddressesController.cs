using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;


namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class AddressesController : ApiController
    {


        Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [Route("api/Addresses")]
        public IHttpActionResult Get()
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                IEnumerable<Address> ad = mapper.GetAddresses();
                if (ad != null)
                {
                    return Ok(mapper.GetAddresses());
                }
                else
                {
                    return NotFound();
                }
            }
            return new ForbiddenActionResult(Request, "access denied");
        }

        [Route("api/Addresses/{id}")]
        public IHttpActionResult Get(long id)
        {

            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            else
            {
                var result = mapper.GetAddress(id);
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

        public void Post(Address data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateAddress(data);
            }
                
        }

        public void Put(long id, Address data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.UpdateAddress(id, data);
            }
               
        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteAddress(id);
            }
                
        }

    }
}