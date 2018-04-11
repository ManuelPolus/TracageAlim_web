using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace TracageAlmentaireWeb.BL.Components
{
    public class TokenGenerator
    {

        public static string GenerateToken()
        {
            var APIKey = "";

            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                APIKey = Convert.ToBase64String(secretKeyByteArray);
            }

            return APIKey;
        }

    }
}