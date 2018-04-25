using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.Controllers
{
    public class ProductsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");
        private APIKeyMessageHandler keyHandler = new APIKeyMessageHandler();

        [System.Web.Http.Route("api/Products")]
        public IHttpActionResult Get()
        {
            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied madafaka");

            }
            return Ok(mapper.GetProducts());
        }

        [System.Web.Http.Route("api/products/{qr}")]
        public IHttpActionResult Get(string qr)
        {

            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied madafaka");
            }
            try
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
            catch (Exception e)
            {
                return NotFound();
            }

        }


        public void Post(Product data)
        {
            mapper.CreateProduct(data);
        }

        [System.Web.Http.Route("api/update/products/{qr}")]
        public void Put(string qr, Product data)
        {
            try
            {
                data.CurrrentTreatmentId = data.CurrentTreatment.Id;
            }
            catch (Exception e)
            {

            }

            mapper.UpdateProduct(qr, data);
            mapper.UpdateProcess(data.ProcessId, data.Process);

        }

        public void Delete(long id)
        {
            mapper.DeleteProduct(id);
        }
    }
}
