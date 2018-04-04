﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.BL.Entities
{
    [Serializable]
    public class EntiteOrganisation
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nom { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telephone { get; set; }
    }
}