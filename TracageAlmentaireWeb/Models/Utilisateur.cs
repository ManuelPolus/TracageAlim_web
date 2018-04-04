using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class Utilisateur
    {
        public int Id { get; set;  }

        public string Nom { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public string Telephone { get; set; }

        public Adresse Adresse { get; set; }

        public Role CurrentRole { get; set; }
    }
}
