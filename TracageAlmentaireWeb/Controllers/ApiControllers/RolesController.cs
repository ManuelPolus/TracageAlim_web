using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class RolesController : ApiController
    {

        FoodTrackerDao<EntiteRole> dao = new FoodTrackerDao<EntiteRole>("RolesUtilisateurs");

        [Route("api/Roles")]
        public IEnumerable<EntiteRole> Get()
        {
            return dao.Get();
        }

        [Route("api/Role/{identifier}")]
        public EntiteRole Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteRole data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteRole data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}