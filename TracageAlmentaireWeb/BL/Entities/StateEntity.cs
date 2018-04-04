using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace TracageAlmentaireWeb.BL.Entities
{
    public class StateEntity
    {
        public long Id { get; set; }

        public string Status { get; set; }

    }
}