using System.Collections.Generic;
using System.Web.Http;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class ScansController : ApiController
    {
        Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        // GET: Scans
        [Route("api/Scans/{productId}")]
        public IHttpActionResult Get(long productId)
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }

            List<Scan> scans = mapper.GetScanByProduct(productId);

            return scans != null ? (IHttpActionResult)Ok(scans) : NotFound();
        }

        public bool Save(Scan s)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateScan(s);
                return true;
            }

            return false;
        }


    }
}