using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Web.Http;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.Controllers.ApiControllers
{

    public class ResourcesController : ApiController
    {
        [Route("api/Resources")]
        public IHttpActionResult Get()
        {
            List<Resource> resourcesList = new List<Resource>();

            // make a db request to get the data

            if (resourcesList.Count != 0)
            {
                return Ok(resourcesList);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/Resources/{name}")]
        public IHttpActionResult Get(string name)
        {
            Resource searchedResource = new Resource();

            //Make a db request to get the data

            if (searchedResource != null)
            {
                return Ok(searchedResource);
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult Post(Resource resourceToSave)
        {
            try
            {
                //make a db Request to save the data
                return Ok();
            }
            catch (DbException exception)
            {
                // exception being thrown by the class that makes the DB requests
                // usually you send an adapted message and not your exception StackTrace, but this is an example.
                return BadRequest(exception.StackTrace);
            }
            catch (Exception otherException)
            {
                //Can make multiple catch statments if the db request class methods throws adapted exceptions.
                //example here, the data has not been found.
                return NotFound();

            }
        }

        public IHttpActionResult Put(string nameToSearchBy, Resource resourceToUpdate)
        {
            Resource resourceInDatabase = new Resource();
            try
            {
                Resource matchingResource = new Resource();
                // make a db request to find the matching name resource to update
                if (matchingResource == null)
                {
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.StackTrace);
            }

            if (resourceInDatabase != resourceToUpdate)
            {
                try
                {
                    //make a db request to update the found resource
                    return Ok();
                }
                catch (Exception otherException)
                {
                    return BadRequest(otherException.StackTrace);
                }
            }
            else
            {
                return Ok(" resource already up to date");
            }

        }

        public IHttpActionResult Delete(string name)
        {

            try
            {
                //make a db request to delete the Resource with the specified name
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.StackTrace);
            }
        }

    }
}
