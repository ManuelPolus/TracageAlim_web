using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class AddressesController : ApiController
    {


        Mapper mapper = new Mapper("FTDb");

        [Route("api/Addresses")]
        public IEnumerable<Address> Get()
        {
            return mapper.GetAddresses();
        }

        [Route("api/Addresses/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetAddress(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(Address data)
        {
            mapper.CreateAddress(data);
        }

        public void Put(long id, Address data)
        {
            mapper.UpdateAddress(id,data);
        }

        public void Delete(long id)
        {
            mapper.DeleteAddress(id);
        }

    }
}