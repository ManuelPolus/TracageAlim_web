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
        FoodTrackerDao<EntiteProduit> dao = new FoodTrackerDao<EntiteProduit>("Produits");

        [Route("api/Produits")]
        public IEnumerable<EntiteProduit> Get()
        {
           return dao.Get();
        }

        [Route("api/Produit/{identifier}")]
        public EntiteProduit Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteProduit data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteProduit data)
        {
            dao.Update(data,identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier); 
        }
    }
}
