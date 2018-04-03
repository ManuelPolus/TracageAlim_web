using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LinqToDB.Mapping;

namespace TracageAlmentaireWeb.BL.Entities
{
    public class EntiteProduit
    {
        [PrimaryKey, MaxLength(900)]
        public string QRCode { get; set; }

        [MaxLength (100)]
        public string Nom { get; set; }

        [MaxLength(100)]
        public string Etat { get; set; }

        public long Id { get; set; }
    }
}