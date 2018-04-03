﻿using System;
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

        //TODO : coller ces méthodes dans les autres api controllers en adaptant les routes
       [Route("api/Produits/{identifier}")]
        public IHttpActionResult Get(string identifier)
        {
            var result = dao.GetByIdentifier(identifier);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("api/Produits/{identifier}/{identifierName}")]
        public IHttpActionResult Get(string identifier, string identifierName)
        {
            var result = dao.GetByIdentifier(identifier, identifierName);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }


        public void Post(EntiteProduit data)
        {
            dao.Add(data);
        }

        public void Put(object identifier, EntiteProduit data)
        {
            dao.Update(data, identifier);
        }

        public void Delete(object identifier)
        {
            dao.Delete(identifier);
        }
    }
}
