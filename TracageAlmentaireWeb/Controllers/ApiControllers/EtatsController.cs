using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class EtatsController : ApiController
    {

        FoodTrackerDao<EntiteEtat> dao = new FoodTrackerDao<EntiteEtat>("etats");

        [Route("api/Etats")]
        public IEnumerable<EntiteEtat> Get()
        {
            return dao.Get();
        }

        [Route("api/Etat/{identifier}")]
        public EntiteEtat Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteEtat data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteEtat data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}