using System;
using System.Security.Cryptography;
using System.Text;

namespace AlimBlockChain
{
    public class BlockHasher
    {

        public static string calculateHash(Block block)
        {
            if (block == null)
            {
                throw new NullReferenceException("Your block shouldn't be null");
            }

            string hashedString = String.Empty;

            try
            {
                string text = block.Str();

                byte[] bytes = Encoding.UTF8.GetBytes(text);
                SHA256Managed sha256Hasher = new SHA256Managed()
                    ;
                byte[] hash = sha256Hasher.ComputeHash(bytes);



                foreach (byte b in hash)
                {
                    hashedString += String.Format("{0:x2}", b);
                }
            }
            catch (Exception e)
            {
                //Hashing went wrong
                Console.WriteLine(e.StackTrace);
            }
            

            return hashedString;
        }
    }
}
