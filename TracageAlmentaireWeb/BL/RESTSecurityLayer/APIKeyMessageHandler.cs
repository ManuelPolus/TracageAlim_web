using System;
using System.Net.Http;
using PasswordHasher = TracageAlmentaireWeb.BL.Components.PasswordHasher;

namespace TracageAlmentaireWeb.BL.RESTSecurityLayer
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        private const string APIKeyToCheck = "$*aT9L5$fsgg(10fV2ljv[CmlB.U)z";

        public  bool CheckApiKey(HttpRequestMessage httpRequestMessage)
        {
            bool validKey = false;
            try
            {
               
                var checkAPIKey = httpRequestMessage.Headers.Authorization;
                string encryptedKey = PasswordHasher.HashKey(APIKeyToCheck);

                if (checkAPIKey.Scheme == "APIKey" && checkAPIKey.Parameter == encryptedKey)
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