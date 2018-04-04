using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LinqToDB;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.DAL
{
    public class TrackingDataContext : DbContext
    {
        public TrackingDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        //Sets
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Processus> Processes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Treatement> Treatements { get; set; }
        public DbSet<Utilisateur> Users { get; set; }
        public DbSet<Scan> Scans { get; set; }
        public DbSet<ProductStateDefinition> ProductStateDefinitions { get; set; }
        public DbSet<SubContractor> SubContractors { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // tables
            modelBuilder.Entity<Adresse>().ToTable("Adresses");
            modelBuilder.Entity<Organisation>().ToTable("Organisations");
            modelBuilder.Entity<Processus>().ToTable("processes");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Step>().ToTable("Steps");
            modelBuilder.Entity<Treatement>().ToTable("Treatements");
            modelBuilder.Entity<Utilisateur>().ToTable("Users");
            modelBuilder.Entity<Scan>().ToTable("Scanner");
            modelBuilder.Entity<ProductStateDefinition>().ToTable("posseder");
            modelBuilder.Entity<SubContractor>().ToTable("SubTractors");

            //keys

            modelBuilder.Entity<Adresse>().HasKey(a => a.Id);
            modelBuilder.Entity<Organisation>().HasKey(o=> o.Id);
            modelBuilder.Entity<Processus>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<Utilisateur>().HasKey(u => u.Id);
            modelBuilder.Entity<Scan>().HasKey(s => new{s.TreatmentId,s.UserId});
            modelBuilder.Entity<ProductStateDefinition>().HasKey(psd => new {psd.ProductId,psd.Stateid});
            modelBuilder.Entity<SubContractor>().HasKey(sc => sc.Id);

            //Required
            modelBuilder.Entity<Adresse>().Property(a => a.Numero).IsRequired();
            modelBuilder.Entity<Adresse>().Property(a => a.CodePostal).IsRequired();
            modelBuilder.Entity<Adresse>().Property(a => a.Pays).IsRequired();
            modelBuilder.Entity<Adresse>().Property(a => a.Rue).IsRequired();


            modelBuilder.Entity<Scan>().Property(s => s.TreatmentId).IsRequired();
            modelBuilder.Entity<Scan>().Property(s => s.UserId).IsRequired();
            modelBuilder.Entity<Scan>().Property(s => s.DateOfScan).IsRequired();

            modelBuilder.Entity<ProductStateDefinition>().Property(psd => psd.ProductId).IsRequired();
            modelBuilder.Entity<ProductStateDefinition>().Property(psd => psd.Stateid).IsRequired();
            modelBuilder.Entity<ProductStateDefinition>().Property(psd => psd.AcquisitionDate).IsRequired();



            //A product goes through Many States
            modelBuilder.Entity<Product>()
                .HasMany(p => p.States)
                .WithMany(s => s.ProductsConcerned);

            //Process -> Steps -> Treatements
            modelBuilder.Entity<Processus>()
                .HasMany(p => p.Etapes);
            modelBuilder.Entity<Step>()
                .HasMany(s => s.Treatements);


            // Treatement -> outgoing state
            modelBuilder.Entity<Treatement>()
                .HasRequired(t => t.OutgoingState);


            //Organisation -> SubContractors
            modelBuilder.Entity<Organisation>()
                .HasMany(o => o.SubContractors);
            modelBuilder.Entity<Organisation>()
                .HasMany(o => o.Workers);
            modelBuilder.Entity<Organisation>()
                .HasOptional(o => o.Adresse);
            modelBuilder.Entity<Organisation>()
                .HasMany(o => o.Processes);

            //SubContractors -> Steps
            modelBuilder.Entity<SubContractor>()
                .HasMany(s => s.StepsInCharge);
            modelBuilder.Entity<SubContractor>()
                .HasOptional(s => s.Adress);
            modelBuilder.Entity<SubContractor>()
                .HasMany(s => s.Workers);

            //Users -> roles
            modelBuilder.Entity<Utilisateur>()
                .HasRequired(u => u.CurrentRole);



        }
    }
}