using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers
{
    public class ProductsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");

        [Route("api/Products")]
        public IEnumerable<Product> Get()
        {
            return mapper.GetProducts();
        }

        [Route("api/products/{qr}")]
        public IHttpActionResult Get(string qr)
        {
            var result = mapper.GetProductByQr(qr);
            result.Process = mapper.GetProcess(result.ProcessId);
            result.CurrentTreatment = mapper.GetTreatment(result.CurrrentTreatmentId);
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

        [Route("api/update/products/{qr}")]
        public void Put(string qr, Product data)
        {
            data.CurrrentTreatmentId = data.CurrentTreatment.Id;
            foreach (var state in data.States)
            {
                data.StatesIds = new List<long>();
                data.StatesIds.Add(state.Id);
            } 
            mapper.UpdateProduct(qr,data);
            mapper.UpdateProcess(data.ProcessId,data.Process);

        }

        public void Delete(long id)
        {
            mapper.DeleteProduct(id);
        }
    }
}
