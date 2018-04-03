using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.BL.Entities
{
    public class EntiteRole
    {
        [Key, MaxLength(50)]
        public string Nom { get; set; }

        [MaxLength(500)]
        public string DescriptionRole { get; set; }
    }
}