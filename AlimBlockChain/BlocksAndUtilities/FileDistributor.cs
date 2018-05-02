using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AlimBlockChain.BlocksAndUtilities
{
    public class FileDistributor
    {

        public void SaveBlockChain(BlockChain bc,string product)
        {
            ValidityGranter vg = new ValidityGranter(bc);
            string bcAsText = bc.toString();

            if (!Directory.Exists("C:\\\\TEMP\\Blockchain\\"))
            {
                Directory.CreateDirectory("C:\\\\TEMP\\Blockchain\\");
            }

            string path = "C:\\\\TEMP\\BlockChain\\"+ product +".txt";

            File.WriteAllText(path, bcAsText+"\n" +"Blockchain is valid :"+vg.IsBlockChainValid());
        }


        public void GetBlockChainFile(string product)
        {
            Process.Start("notepad.exe", @"C:\\TEMP\Blockchain\"+product+".txt");
        }

    }
}
