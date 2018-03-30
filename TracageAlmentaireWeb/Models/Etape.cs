using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class Etape
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public int Position { get; set; }

        public List<Traitement> Traitements { get; set; }


    }
}
