using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class AdressesController : ApiController
    {

        FoodTrackerDao<EntiteAdresse> dao = new FoodTrackerDao<EntiteAdresse>("adresse");

        [Route("api/Adresses")]
        public IEnumerable<EntiteAdresse> Get()
        {
            return dao.Get();
        }

        [Route("api/Adresse/{identifier}")]
        public EntiteAdresse Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteAdresse data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteAdresse data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}