﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{
    public class TreatmentsController : ApiController
    {
        private Mapper mapper = new Mapper("FTDb");


        [Route("api/Treatments")]
        public IEnumerable<Treatment> Get()
        {
            return mapper.GetTreatments();
        }

        [Route("api/Treatments/{id}")]
        public IHttpActionResult Get(long id)
        {
            var result = mapper.GetTreatment(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public void Post(Treatment data)
        {
            mapper.CreateTreatment(data);
        }

        public void Put(long id, Treatment data)
        {
            mapper.UpdateTreatment(id, data);
        }

        public void Delete(long id)
        {
            mapper.DeleteTreatment(id);
        }
    }
}