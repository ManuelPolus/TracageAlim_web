using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tracage.Models;

namespace TracageAlmentaireWeb.Controllers
{
    public class FoodTrackerContext : DbContext
    {



        public FoodTrackerContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }




    }
}