using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NodeApp
{
    class RestClient
    {
        private readonly HttpClient _client;
        private const string Resturl = "https://e30d5cbd.ngrok.io/api/node/begin"; //TODO replace with real 


        public RestClient()
        {
            _client = new HttpClient();
        }

        public IEnumerable<string> GetDataAsync()
        {
            List<string> items = new List<string>();
            var uri = new Uri(string.Format(Resturl, string.Empty));
            var response = _client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<string>>(content);
            }

            return items;
        }

        public string GetDataAsync(string resource)
        {
            string item = string.Empty;
            var uri = new Uri(string.Format(Resturl+"/"+resource+"/", string.Empty));
            var response = _client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<string>(content);
            }

            return item;
        }
    }
}
