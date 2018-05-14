using AlimBlockChain;
using AlimBlockChain.BlocksAndUtilities;
using System;
using System.Diagnostics;
using System.IO;

namespace Node.FileUtility
{
    public class FileReader
    {
        public void SaveBlockChain(BlockChain bc, string product)
        {
            ValidityGranter vg = new ValidityGranter(bc);
            string bcAsText = bc.toString();

            if (!Directory.Exists("C:\\AlimBlockChain\\Data\\"))
            {
                Directory.CreateDirectory("C:\\AlimBlockChain\\Data\\");
            }

            string path = "C:\\AlimBlockChain\\Data\\" + product + ".txt";

            File.WriteAllText(path, bcAsText + "\n" + "Blockchain is valid :" + vg.IsBlockChainValid());
            File.WriteAllText("C:\\TEMP\\Blockchain", bcAsText + "\n" + "Blockchain is valid :" + vg.IsBlockChainValid());
        }




    }
}