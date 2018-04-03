using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.BL.Entities
{
    public class EntiteTraitement
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nom { get; set; }

        [MaxLength(1000)]
        public string DescriptionEtape { get; set; }

        public int Position { get; set; }

        public int IdEtape { get; set; }

        public int IdEtatSortant { get; set; }
    }
}