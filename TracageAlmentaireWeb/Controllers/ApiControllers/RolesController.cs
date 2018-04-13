using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class RolesController : ApiController
    {

       Mapper mapper =new Mapper("FTDb");
        
        [Route("api/Roles")]
        public IEnumerable<Role> Get()
        {
            return mapper.GetRoles();
        }

        [Route("api/Roles/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetRole(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

       
        public void Post(Role data)
        {
            mapper.CreateRole(data);
        }

        public void Put(long id, Role data)
        {
            mapper.UpdateRole(id,data);
        }

        public void Delete(long id)
        {
            mapper.DeleteRole(id);
        }

    }
}