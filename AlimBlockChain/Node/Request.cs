
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Server
{
    public class Request
    {
        public string Type { get; set; }
        private string URL { get; set; }
        public string Host { get; set; }

        public Request(string type, string url, string host)
        {
            Type = type;
            URL = url;
            Host = host;
        }


        private static Request GetRequest(string request)
        {
            if (string.IsNullOrEmpty(request))
                return null;

            string[] tokens = request.Split(' ');
            return new Request("yo","bebe","tkt");

        }


    }
}
