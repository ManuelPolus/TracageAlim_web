using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCServer
{
    public class HttpServer
    {
        private bool running;
        private TcpListener listener;

        public HttpServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            running = true;
            listener.Start();
            while (running)
            {
                Console.WriteLine("waiting for connection...");

                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("connected !");

                HandleClient(client);

                client.Close();
            }
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            string message = string.Empty;

            while (reader.Peek() != -1)
            {
                message += reader.ReadLine() + "\n";
            }
            Console.WriteLine("\n request : " + message);
        }
    }
}
