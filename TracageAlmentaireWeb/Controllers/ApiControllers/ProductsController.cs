using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AlimBlockChain;
using AlimBlockChain.BlocksAndUtilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                return new ForbiddenActionResult(Request, "access denied");

            }
            return Ok(mapper.GetProducts());
        }

        [System.Web.Http.Route("api/products/{qr}")]
        public IHttpActionResult Get(string qr)
        {

            if (!keyHandler.CheckApiKey(this.Request))
            {
                return new ForbiddenActionResult(Request, "access denied");
            }
            try
            {
                var result = mapper.GetProductByQr(qr);
                result.Process = mapper.GetProcess(result.ProcessId);
                result.CurrentTreatment = mapper.GetTreatment(result.CurrrentTreatmentId);
                if (result != null)
                {
                    #region bc test

                    if (result.CurrentTreatment != null && result.CurrentTreatment.OutgoingState.Final)
                    {                    
                        BlockChain bc = new BlockChain(2);
                        string productAstring = JsonConvert.SerializeObject(result);

                        Block b = bc.NewBlock(productAstring);
                        bc.AddBlock(b);
                        Miner.MineBlock(1,bc.Blocks.ElementAt(0));
                        Miner.MineBlock(1, b);
                        FileDistributor fd = new FileDistributor();
                        fd.SaveBlockChain(bc, result.QRCode);
                        fd.GetBlockChainFile(result.QRCode);
                    }

                    #endregion
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
