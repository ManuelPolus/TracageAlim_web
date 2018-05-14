using Node.FileUtility;
using System;
using System.Web.Http;

namespace Node.Controllers
{
    public class BlockChainController : ApiController
    {

        [Route("api/node/begin")]
        public IHttpActionResult BecomeNode()
        {
            try
            {



                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }

        [Route("api/node/get/{qr}")]
        public IHttpActionResult GET(string qr)
        {
            string content = FileReader.GetBlockChainFileContent(qr);
            return Ok(content);
        }
        


    }
}
