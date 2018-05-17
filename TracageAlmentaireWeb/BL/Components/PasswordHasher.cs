using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
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

        public static string HashKey(string input)
        {
            SHA512 shaM = new SHA512Managed();

            byte[] data = shaM.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            input = sBuilder.ToString();
            return (input);
        }

        public bool CheckKey(string localKey,string externalKey)
        {
            string encryptedKey = HashKey(localKey);
            return encryptedKey == externalKey;
        }
    }
}