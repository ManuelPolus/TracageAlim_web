using AlimBlockChain;
using AlimBlockChain.BlocksAndUtilities;
using Newtonsoft.Json;
using System;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.RESTSecurityLayer;
using TracageAlmentaireWeb.DAL;

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
                return new ForbiddenActionResult(Request, "access denied");

            }
            return Ok(mapper.GetProducts());
        }

        [Route("api/products/{qr}")]
        public IHttpActionResult Get(string qr)
        {

            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }

            try
            {
                var result = mapper.GetProductByQr(qr);

                if (result != null)
                {
                    result.Process = mapper.GetProcess(result.ProcessId);
                    result.CurrentTreatment = mapper.GetTreatment(result.CurrrentTreatmentId);

                    #region bc test

                    if (result.CurrentTreatment != null && result.CurrentTreatment.OutgoingState.Final)
                    {
                        BlockChain bc = new BlockChain(2);

                        foreach (var step in result.Process.Steps)
                        {
                            string stateAsString = JsonConvert.SerializeObject(step);
                            Block bs = bc.NewBlock(stateAsString);
                            bc.AddBlock(bs);
                        }

                        FileDistributor fd = new FileDistributor();
                        fd.SaveBlockChain(bc, result.QRCode);
                    }

                    #endregion

                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        public void Post(Product data)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.CreateProduct(data);
            }

        }

        [System.Web.Http.Route("api/update/products/{qr}")]
        public void Put(string qr, Product data)
        {
            if (keyHandler.CheckApiKey(this.Request))
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

        }

        public void Delete(long id)
        {
            if (keyHandler.CheckApiKey(this.Request))
            {
                mapper.DeleteProduct(id);
            }
        }
    }
}

