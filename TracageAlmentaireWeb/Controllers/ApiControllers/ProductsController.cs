using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers
{
    public class ProductsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");

        [Route("api/Produits")]
        public IEnumerable<Product> Get()
        {
            return mapper.GetProducts();
        }

        
       [Route("api/Produits/{id}")]
        public IHttpActionResult Get(long id)
       {
           var result = mapper.GetProduct(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }


        public void Post(Product data)
        {
            mapper.CreateProduct(data);
        }

        public void Put(long id, Product data)
        {
            mapper.UpdateProduct(id,data);
        }

        public void Delete(long id)
        {
            mapper.DeleteProduct(id);
        }
    }
}
