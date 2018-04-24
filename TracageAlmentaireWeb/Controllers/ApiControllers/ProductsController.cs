using System;
using System.Collections.Generic;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

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
            try
            {
                var result = mapper.GetProductByQr(qr);
                result.Process = mapper.GetProcess(result.ProcessId);
                result.CurrentTreatment = mapper.GetTreatment(result.CurrrentTreatmentId);
                result.States = new List<State>();
                foreach (var stateId in result.StatesIds)
                {
                    result.States.Add(mapper.GetState(stateId));
                }
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return NotFound();
            }
            
        }


        public void Post(Product data)
        {
            mapper.CreateProduct(data);
        }

        [Route("api/update/products/{qr}")]
        public void Put(string qr, Product data)
        {
            try
            {
                data.CurrrentTreatmentId = data.CurrentTreatment.Id;
            }
            catch (Exception e)
            {

            }
            
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
