using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc.Async;

namespace TracageAlmentaireWeb.BL.RESTSecurityLayer
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        private const string APIKeyToCheck = "bonsoir";

        public  bool CheckApiKey(HttpRequestMessage httpRequestMessage)
        {
            bool validKey = false;
            try
            {
               
                var checkAPIKey = httpRequestMessage.Headers.Authorization;

                if (checkAPIKey.Scheme == "APIKey" && checkAPIKey.Parameter == APIKeyToCheck)
                    validKey = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                //should be triggered by httprequest without auth header
            }
            

            return validKey;
        }
    }
}