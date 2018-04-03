using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TracageAlmentaireWeb.BL.Entities
{
    [Serializable]
    public class EntiteEtape
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nom { get; set; }

        [MaxLength(1000)]
        public string DescriptionEtape { get; set; }

        public int Position { get; set; }

        public long IdSousTraitant { get; set; }

        public long IdProcess { get; set; }


    }
}