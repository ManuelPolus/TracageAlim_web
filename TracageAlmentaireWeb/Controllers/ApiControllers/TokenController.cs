using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Activation;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Components;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    //fe734702-a3c3-4ca9-a9c5-efa45913bc33
    public class TokenController //s: ApiController
    {

        [Route("/token")]
        public string GetToken()
        {
            return TokenGenerator.GenerateToken();
        }

    }
}
