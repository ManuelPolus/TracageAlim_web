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

        public EntiteProduit(string nom,string etat)
        {
            Nom =nom;
            Etat = etat;
            this.QRCode = "TA-"+Id +"-"+ Nom;
        }

        [MaxLength(900)]
        public string QRCode { get; set; }

        [MaxLength (100)]
        public string Nom { get; set; }

        [MaxLength(100)]
        public string Etat { get; set; }

        [PrimaryKey]
        public long Id { get; set; }
    }
}