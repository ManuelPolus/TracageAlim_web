using AlimBlockChain.BlocksAndUtilities;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Node.Controllers
{
    public class BlockChainController : ApiController
    {

        [Route("api/node/begin")]
        public IHttpActionResult GET()
        {
            try
            {
                List<string> bcList = new List<string>();
                bcList = FileDistributor.GetBlockChains();
                return Ok(bcList);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }

        [Route("api/node/get/{qr}")]
        public IHttpActionResult GET(string qr)
        {
            string content = FileDistributor.GetBlockChainFileContent(qr);
            return Ok(content);
        }
        


    }
}
