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




        private void TryCreateDirectory()
        {
            if (!Directory.Exists(repository))
            {
                Directory.CreateDirectory(repository);
            }
        }


    }
}
