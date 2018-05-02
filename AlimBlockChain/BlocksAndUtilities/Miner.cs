using System;


namespace AlimBlockChain
{
     public class Miner
    {

        public static void MineBlock(int difficulty, Block block)
        {
            block.Nonce = 0;

            while (!block.Hash.Substring(0, difficulty).Equals(Zeros(difficulty)))
            {
                block.Nonce++;
                block.Hash = BlockHasher.calculateHash(block);
            }
        }


        //This method writes Zeros... Isn' life f*cking beautiful
        private static string Zeros(int number)
        {
            if (number < 1)
                throw new Exception("Go f*ck yourself dude");

            string zeros = "";

            for (int i = 0; i < number; i++)
            {
                zeros += "0";
            }

            return zeros;
        }

    }
}
