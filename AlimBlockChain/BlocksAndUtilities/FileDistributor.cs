using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace AlimBlockChain.BlocksAndUtilities
{
    public class FileDistributor
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

            File.WriteAllText(path, product + "&bc" + bcAsText + "\n" + "Blockchain is valid :" + vg.IsBlockChainValid());
        }


        public static List<string> GetBlockChains()
        {
            List<string> bcList = new List<string>();
            foreach (string file in Directory.EnumerateFiles("C:\\AlimBlockChain\\Data\\", "*.txt"))
            {
                string contents = File.ReadAllText(file);
                bcList.Add(contents);
            }
            return bcList;
        }

        public void GetBlockChainFile(string product)
        {
            Process.Start("notepad.exe", @"C:\\AlimBlockChain\\Data\\" + product + ".txt");
        }

        public static string GetBlockChainFileContent(string product)
        {
            try
            {
                return File.ReadAllText("C:\\AlimBlockChain\\Data\\" + product + ".txt");
            }
            catch (Exception e)
            {
                return e.StackTrace;
            }

        }

    }
}
