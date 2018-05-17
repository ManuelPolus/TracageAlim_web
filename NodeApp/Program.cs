using System;
using System.Collections.Generic;
using System.Linq;

namespace NodeApp
{
    public class Program
    {
        private static FileManager manager = new FileManager();
        private static RestClient client = new RestClient();

        public static void Main(string[] args)
        {
            Console.WindowWidth = 150;
            Console.Title = "Alim block chain Node application";
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            string request = "";
            while (request != "exit")
            {
                Console.WriteLine("Type your commands :");
                Console.Write("->");
                request = Console.ReadLine();
                switch (request)
                {
                    case "download":
                        Download();
                        break;

                    case "update":
                        Update();
                        break;

                    case "check":
                        Check(false);
                        break;

                    case "check -u":
                        Check(true);
                        break;


                    case "help":
                        DisplayHelp();
                        break;
                }
            }
        }

        private static void Download()
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
                Console.WriteLine("\n--");
                Console.WriteLine("download went ok");
                Console.WriteLine("--\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n--");
                Console.WriteLine("Something went wrong");
                Console.WriteLine("--\n");
            }

        }

        private static void Update()
        {

            try
            {
                List<string> resources = new List<string>();
                int i = 0;

                resources = client.GetDataAsync().ToList();

                foreach (var bc in resources)
                {
                    string fileName = bc.Substring(0, bc.IndexOf("&bc"));

                    if (!manager.CheckFileExist(fileName))
                    {
                        manager.WriteFile(fileName, bc);
                        i++;
                    }
                }
                if (i == 0)
                {
                    Console.WriteLine("\n--");
                    Console.WriteLine("Everything is up to date :)");
                    Console.WriteLine("--\n");
                }
                else
                {
                    Console.WriteLine("\n--");
                    Console.WriteLine("Update done succesfully : " + i + " elements updated");
                    Console.WriteLine("--\n");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("\n--");
                Console.WriteLine("Something went wrong");
                Console.WriteLine("--\n");
            }

        }

        //TODO if file doesnt exist notify bcs not up-to-date
        private static bool Check(bool update)
        {
            try
            {

                if (update)
                {
                    Console.WriteLine("Updating your local data...");
                    Update();
                }

                List<string> serverResources = new List<string>();
                List<string> localResources = new List<string>();

                serverResources = client.GetDataAsync().ToList();
                localResources = manager.GetBlockChains();

                Dictionary<string, string> serverDictionary = new Dictionary<string, string>();
                Dictionary<string, string> localDictionary = new Dictionary<string, string>();

                foreach (string s in serverResources)
                {
                    string fileName = s.Substring(0, s.IndexOf("&bc"));
                    serverDictionary.Add(fileName, s);
                }

                foreach (string s in localResources)
                {
                    string fileName = s.Substring(0, s.IndexOf("&bc"));
                    localDictionary.Add(fileName, s);
                }

                serverResources = client.GetDataAsync().ToList();
                localResources = manager.GetBlockChains();

                Console.WriteLine("Comparing blockchains...");
                int i = 0;
                foreach (var bc in serverDictionary)
                {
                    var foundElement = localDictionary.First(o => o.Key == bc.Key);

                    //Case they are missing files
                    if (foundElement.Key == null && foundElement.Value ==null)
                    {
                        
                    }

                    if (!(bc.Value == foundElement.Value))
                    {
                        Console.WriteLine(" Blockchain corrupted : " + foundElement.Key);
                        i++;
                    }

                }


                if (i > 0)
                {
                    Console.WriteLine("\n--");
                    Console.WriteLine("Some blockchains aren't valid :" + i + " elements found to be corrupted");
                    Console.WriteLine("--\n");
                    return false;
                }
                Console.WriteLine("\n--");
                Console.WriteLine("Everything is ok !");
                Console.WriteLine("--\n");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("--/!\\-- They are missing files on your computer. Your blockchain is not up-to-date");
                Console.WriteLine("------- try updating your data by using the 'update' cmd line or by using the '-u' option on the 'check' cmd line");
                Console.WriteLine();
                return false;
            }
        }

        private async static void DisplayHelp()
        {
            Console.WriteLine("--- 'download' - download blochain : only use if you don't have a blockchain version. Performance is then better with update");
            Console.WriteLine("--- 'update'   - update you alim blockchain version ");
            Console.WriteLine("--- 'exit'     - Exit the program");
        }

        //Unusefull for now
        private static bool CheckBlockChain(string fileName)
        {
            if (!manager.CheckFileExist(fileName))
            {
                Console.WriteLine("Your file doesnt exist...");
                return false;
            }

            string localChain = manager.ReadFile(fileName);
            string serverChain = client.GetDataAsync(fileName);

            if (localChain == serverChain)
            {
                return true;
            }

            return false;
        }

    }
}