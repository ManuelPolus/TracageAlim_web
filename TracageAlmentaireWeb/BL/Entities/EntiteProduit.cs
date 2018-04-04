using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using LinqToDB.Mapping;

namespace TracageAlmentaireWeb.BL.Entities 
{
    [Serializable]
    public class EntiteProduit
    {

        public EntiteProduit(string nom,EntiteEtat initialState)
        {
            Nom =nom;
            Etats.Add(initialState);
            this.QRCode = "TA-"+Id +"-"+ Nom;
        }

        [MaxLength(900)]
        public string QRCode { get; set; }

        [MaxLength (100)]
        public string Nom { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public List<EntiteEtat> Etats { get; set; }

        [PrimaryKey]
        public long Id { get; set; }
    }
}