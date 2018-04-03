using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LinqToDB;
using TracageAlmentaireWeb.BL.Entities;

namespace TracageAlmentaireWeb.DAL
{
    public class TrackingDataContext : DbContext
    {
        public TrackingDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        public DbSet<EntiteEtat> Etats { get; set; }
        public DbSet<EntiteRole> Roles { get; set; }
        public DbSet<EntiteAdresse> Adresses { get; set; }
        public DbSet<EntiteEtape> Etapes { get; set; }

    }
}