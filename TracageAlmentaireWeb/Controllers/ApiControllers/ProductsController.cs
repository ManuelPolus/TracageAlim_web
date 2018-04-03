using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers
{
    public class ProductsController : ApiController
    {
        FoodTrackerDao<Produit> dao = new FoodTrackerDao<Produit>();

        [Route("api/Produits")]
        public IEnumerable<Produit> Get()
        {
           return dao.All();
        }

        public Produit Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(Produit data)
        {
            dao.Add(data);
        }

        public void Put(object identifier,Produit data)
        {
            dao.Update(data,identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier); 
        }
    }
}
