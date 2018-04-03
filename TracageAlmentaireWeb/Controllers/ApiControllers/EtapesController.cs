using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class EtapesController : ApiController
    {
        FoodTrackerDao<EntiteEtape> dao = new FoodTrackerDao<EntiteEtape>("Etapes");

        [Route("api/Etapes")]
        public IEnumerable<EntiteEtape> Get()
        {
            return dao.Get();
        }

        public EntiteEtape Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteEtape data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteEtape data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }
    }

}