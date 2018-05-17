using System.Collections.Generic;
using System.Web.Http;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    
    public class UserScanRightsController : ApiController
    {

        Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();


        [Route("api/UserScanRightss/{roleId}")]
        public IHttpActionResult Get(long roleId)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                IEnumerable<UserScanRights> ad = mapper.GetRightsByRole(roleId);
                if (ad != null)
                {
                    return Ok(ad);
                }
                else
                {
                    return NotFound();
                }
            }
            return new ForbiddenActionResult(Request, "access denied");
        }
    }
}
