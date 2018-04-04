using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.Models
{
    public class ProductStateDefinition
    {
        public long ProductId { get; set; }

        public long Stateid { get; set; }

        public DateTime AcquisitionDate { get; set; }


    }
}