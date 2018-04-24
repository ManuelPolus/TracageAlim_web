using System.Data.Entity;
using Tracage.Models;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.DAL
{
    public class TrackingDataContext : DbContext
    {
        public TrackingDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        //Sets
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Organization> Organisations { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Treatment> Treatements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Scan> Scans { get; set; }
        public DbSet<ProductStateDefinition> ProductStateDefinitions { get; set; }
        public DbSet<SubContractor> SubContractors { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //Database.SetInitializer<TrackingDataContext>(null);
            //base.OnModelCreating(modelBuilder);

            // tables
            modelBuilder.Entity<Address>().ToTable("Adresses");
            modelBuilder.Entity<Organization>().ToTable("Organisations");
            modelBuilder.Entity<Process>().ToTable("processes");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Step>().ToTable("Steps");
            modelBuilder.Entity<State>().ToTable("States");
            modelBuilder.Entity<Treatment>().ToTable("Treatments");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Scan>().ToTable("Scanner");
            modelBuilder.Entity<ProductStateDefinition>().ToTable("posseder");
            modelBuilder.Entity<SubContractor>().ToTable("SubContractors");

            //keys

            modelBuilder.Entity<Address>().HasKey(a => a.Id);
            modelBuilder.Entity<Organization>().HasKey(o=> o.Id);
            modelBuilder.Entity<Process>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Scan>().HasKey(s => new{s.TreatmentId,s.UserId});
            modelBuilder.Entity<ProductStateDefinition>().HasKey(psd => new {psd.ProductId,psd.StateId});
            modelBuilder.Entity<SubContractor>().HasKey(sc => sc.Id);
            modelBuilder.Entity<State>().HasKey(s => s.Id);

            //Required
            modelBuilder.Entity<Address>().Property(a => a.Number).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.PostalCode).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Country).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Street).IsRequired();


            modelBuilder.Entity<Scan>().Property(s => s.TreatmentId).IsRequired();
            modelBuilder.Entity<Scan>().Property(s => s.UserId).IsRequired();
            modelBuilder.Entity<Scan>().Property(s => s.DateOfScan).IsRequired();

            modelBuilder.Entity<ProductStateDefinition>().Property(psd => psd.ProductId).IsRequired();
            modelBuilder.Entity<ProductStateDefinition>().Property(psd => psd.StateId).IsRequired();
            modelBuilder.Entity<ProductStateDefinition>().Property(psd => psd.AcquisitionDate).IsRequired();


            //Process -> Steps -> Treatments
            modelBuilder.Entity<Process>()
                .HasMany(p => p.Steps);
            modelBuilder.Entity<Step>()
                .HasMany(s => s.Treatments);


            // Treatment -> outgoing state
            modelBuilder.Entity<Treatment>()
                .HasRequired(t => t.OutgoingState);


            //Organization -> SubContractors
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.SubContractors);
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Workers);
            modelBuilder.Entity<Organization>()
                .HasOptional(o => o.Address);
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Processes);

            //SubContractors -> Steps
            modelBuilder.Entity<SubContractor>()
                .HasMany(s => s.StepsInCharge);
            modelBuilder.Entity<SubContractor>()
                .HasOptional(s => s.Address);
            modelBuilder.Entity<SubContractor>()
                .HasMany(s => s.Workers);

            //Users -> roles
            modelBuilder.Entity<User>()
                .HasRequired(u => u.CurrentRole);



        }
    }
}