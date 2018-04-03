using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.BL.Entities
{
    [Serializable]
    public class EntiteAdresse
    {

        [Key, MaxLength(5)] public string Numero { get; set; }

        [Key, MaxLength(163)] public string Rue { get; set; }

        [Key, MaxLength(20)] public string CodePostal { get; set; }

        [Key, MaxLength(163)] public string Ville { get; set; }

        [Key, MaxLength(163)] public string Pays { get; set; }

        public long IdUtilisateur { get; set; }

        public long IdSoustraitant { get; set; }

        public long IdOrganisation { get; set; }
    }
}