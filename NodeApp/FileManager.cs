using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeApp
{

    public class FileManager
    {
        private const string repository = "C:\\AlimBlockChain\\Data\\node";


        public void WriteFile(string fileName, string content)
        {
            try
            {
                TryCreateDirectory();

                string path = repository + "\\" + fileName + ".txt";

                File.WriteAllText(path,content);

            }
            catch(Exception e)
            {

            }

        }

        public string ReadFile(string fileName)
        {
            string path = repository + "\\" + fileName + ".txt";
            return File.ReadAllText(path);
        }

        public List<string> GetBlockChains()
        {
            List<string> bcList = new List<string>();
            foreach (string file in Directory.EnumerateFiles(repository, "*.txt"))
            {
                string contents = File.ReadAllText(file);
                bcList.Add(contents);
            }
            return bcList;
        }

        public bool CheckFileExist(string fileName)
        {
            return File.Exists(repository +"\\"+fileName+".txt");
        }

        private void TryCreateDirectory()
        {
            if (!Directory.Exists(repository))
            {
                Directory.CreateDirectory(repository);
            }
        }


    }
}
