using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TracageAlmentaireWeb.Controllers
{
    public class ValuesController<T> : ApiController
    {
        // GET api/values
        public IEnumerable<T> Get()
        {
           return new List<T>();
        }

        public T Get<T>(int id)
        {
            
        }

        // POST api/values
        public void Post(T data)
        {
        }

        // PUT api/values/5
        public void Put(int id,T data)
        {
        }

        // DELETE api/values/5
        public bool Delete(int id)
        {
        }
    }
}
