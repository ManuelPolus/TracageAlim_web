using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class ProcessController : ApiController
    {

        FoodTrackerDao<EntiteProcess> dao = new FoodTrackerDao<EntiteProcess>("process");

        [Route("api/Process")]
        public IEnumerable<EntiteProcess> Get()
        {
            return dao.Get();
        }

        [Route("api/Process/{identifier}")]
        public EntiteProcess Get(object identifier)
        {
            return dao.GetByIdentifier(identifier);
        }

        public void Post(EntiteProcess data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteProcess data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }

    }
}