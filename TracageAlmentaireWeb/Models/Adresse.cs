using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class Adresse
    {
        public long Id { get; set; }
        public string Rue { get; set; }

        public string Numero { get; set; }

        public string CodePostal { get; set; }

        public string Pays { get; set; }


    }
}
