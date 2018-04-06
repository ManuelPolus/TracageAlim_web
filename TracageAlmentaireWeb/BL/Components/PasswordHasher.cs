using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using Tracage.Models;

namespace TracageAlmentaireWeb.BL.Components
{
    public class PasswordHasher
    {

        public static void Hash(User bob)
        {
            var salt = Crypto.GenerateSalt();
            bob.Salt = salt;
            bob.Password = Crypto.HashPassword(bob.Password + salt);
        }

        public static bool CheckPassword(string clearPassword, string encryptedPassword)
        {
           
            return Crypto.VerifyHashedPassword(encryptedPassword, clearPassword);
        }
    }
}