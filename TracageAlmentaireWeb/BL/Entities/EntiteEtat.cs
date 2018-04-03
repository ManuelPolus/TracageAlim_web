using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using LinqToDB.Mapping;

namespace TracageAlmentaireWeb.BL.Entities
{
    [Serializable]
    public class EntiteEtat
    {
        [PrimaryKey]
        public long Id { get; set; }
        [MaxLength(20)]
        public string Status { get; set; }
    }
}