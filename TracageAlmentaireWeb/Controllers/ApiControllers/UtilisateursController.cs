using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class UtilisateursController : ApiController
    {

        FoodTrackerDao<EntiteUtilisateur> dao = new FoodTrackerDao<EntiteUtilisateur>("Utilisateurs");

        [Route("api/Utilisateurs")]
        public IEnumerable<EntiteUtilisateur> Get()
        {
            return dao.Get();
        }

        [Route("api/Utilisateur/{identifier}")]
        public EntiteUtilisateur Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteUtilisateur data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteUtilisateur data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}