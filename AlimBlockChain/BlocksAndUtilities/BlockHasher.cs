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

            string input = String.Empty;

            try
            {
                input = block.Str();

                SHA512 shaM = new SHA512Managed();

                byte[] data = shaM.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                input = sBuilder.ToString();
               
            }
            catch (Exception e)
            {
                //Hashing went wrong
                Console.WriteLine(e.StackTrace);
            }


            return input;
        }
    }
}
