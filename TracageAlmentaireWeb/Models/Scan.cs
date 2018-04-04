using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracage.Models;

namespace TracageAlmentaireWeb.Models
{
    public class Scan
    {
        public long UserId { get; set; }

        public long TreatmentId { get; set; }

        public DateTime DateOfScan { get; set; }

    }
}