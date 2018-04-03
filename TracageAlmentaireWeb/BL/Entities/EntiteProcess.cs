using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.BL.Entities
{
    public class EntiteProcess
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nom { get; set; }

        [MaxLength(1000)]
        public string DescriptionProcess { get; set; }

    }
}