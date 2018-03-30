using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
     public class Produit
    {
        public string QRCode { get; set; }

        public string Nom { get; set; }

        public string Etat { get; set; }

        public string Description { get; set; }

        public Traitement TraitementEnCours { get; set; }
    }
}
