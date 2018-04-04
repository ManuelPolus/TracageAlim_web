using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class SubContractor
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public Adresse Adress { get; set; }

        public List<Step> StepsInCharge { get; set; }

        public List<Utilisateur> Workers { get; set; }
    }
}
