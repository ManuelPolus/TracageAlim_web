using AlimBlockChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeApp
{
    public class Program
    {
        private static FileManager manager = new FileManager();
        private static RestClient client = new RestClient();

        public static void Main(string[] args)
        {
            string request = "";
            while (request != "exit")
            {
                Console.WriteLine("what u wanna do ?");
                request = Console.ReadLine();
                switch (request)
                {
                    case "download":
                        Download();
                        break;

                    case "update":
                        Update();
                        break;
                }
            }



            void Download()
            {
                try
                {
                    List<string> resources = new List<string>();

                    resources = client.GetDataAsync().ToList();

                    foreach (var bc in resources)
                    {
                        string fileName = bc.Substring(0, bc.IndexOf("&bc"));
                        manager.WriteFile(fileName, bc);
                    }
                    Console.WriteLine("download wen ok");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong");
                }

            }

            void Update()
            {

            }

        }
    }

}