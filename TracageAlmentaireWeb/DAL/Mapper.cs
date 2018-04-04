using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http.ModelBinding;
using LinqToDB;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Tracage.Models;
using TracageAlmentaireWeb.Models;
using ModelState = System.Web.Mvc.ModelState;

namespace TracageAlmentaireWeb.DAL
{
    public class Mapper
    {
        private string database;

        private TrackingDataContext context;

        public Mapper(string database)
        {
            this.database = database;
            this.context = new TrackingDataContext(database);
        }

        #region Users

        public List<Utilisateur> GetUsers()
        {
            List<Utilisateur> bobs = new List<Utilisateur>();
            using (context)
            {
                try
                {
                    bobs = context.Users.ToList();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
                return bobs;
            }
        }

        public virtual Utilisateur GetUser(long id)
        {
            Utilisateur bob = new Utilisateur();
            using (context)
            {
                try
                {
                    bob = context.Users.FirstOrDefault(u => u.Id == id);

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
                return bob;
            }
        }


        public void CreateUser(Utilisateur bob)
        {
            using (context)
            {
                try
                {
                    context.Users.Add(bob);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

            }
        }

        public void UpdateUser(long id, Utilisateur newBob)
        {
            Utilisateur bob = new Utilisateur();
            using (context = new TrackingDataContext(database))
            {
                try
                {
                    bob = context.Users.FirstOrDefault(u => u.Id == id);

                    if (! bob.Equals(newBob))
                    {
                        bob.Nom = newBob.Nom;
                        bob.Email = newBob.Email;
                        bob.Adresse = newBob.Adresse;
                        bob.MotDePasse = newBob.MotDePasse;
                        bob.Telephone = newBob.Telephone;
                        bob.CurrentRole = newBob.CurrentRole;

                        context.SaveChanges();
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteUser(long id)
        {
            using (context)
            {
                try
                {
                    context.Users.Delete(u => u.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }


        #endregion

        #region Products

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (context)
            {
                try
                {
                    products = context.Products.ToList();
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
            return products;
        }


        public virtual Product GetProduct(long id)
        {
            Product product = new Product();
            using (context)
            {
                try
                {
                    product = context.Products.FirstOrDefault(p => p.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return product;
            }
        }

        public void CreateProduct(Product newProduct)
        {
            using (context)
            {
                try
                {
                    context.Products.Add(newProduct);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }

        }

        public void UpdateProduct(long id, Product updatedProduct)
        {
            Product product = new Product();

            using (context)
            {
                try
                {
                    product = context.Products.FirstOrDefault(p => p.Id == id);

                    if (!product.Equals(updatedProduct))
                    {
                        product.Id = updatedProduct.Id;
                        product.CurrentTreatement = updatedProduct.CurrentTreatement;
                        product.Description = updatedProduct.Description;
                        product.Name = updatedProduct.Name;
                        product.QRCode = updatedProduct.QRCode;
                        product.States = updatedProduct.States;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }

        }

        public void DeleteProduct(long id)
        {
            using (context)
            {
                try
                {
                    context.Products.Delete(p => p.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }

        }
        #endregion

        #region Treatements

        public List<Treatement> GetTreatments()
        {
            List<Treatement> treatements = new List<Treatement>();

            using (context)
            {
                try
                {
                    treatements = context.Treatements.ToList();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return treatements;

            }

        }

        public Treatement GetTreatment(long id)
        {
            Treatement treatement = new Treatement();

            using (context)
            {
                try
                {
                    treatement = context.Treatements.FirstOrDefault(t => t.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return treatement;
            }

        }

        public void CreateTreatment(Treatement newTreatment)
        {

            using (context)
            {
                try
                {
                    context.Treatements.Add(newTreatment);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
                
        }

        public void UpdateTreatment(long id, Treatement updatedTreatement)
        {
            Treatement treatement = new Treatement();

            using (context)
            {
                try
                {
                    treatement = context.Treatements.FirstOrDefault(t => t.Id == id);

                    if (!treatement.Equals(updatedTreatement))
                    {
                        treatement.Id = updatedTreatement.Id;
                        treatement.Desrciption = updatedTreatement.Desrciption;
                        treatement.Name = updatedTreatement.Name;
                        treatement.OutgoingState = updatedTreatement.OutgoingState;
                        treatement.Position = updatedTreatement.Position;

                        context.SaveChanges();

                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
                
        }

        public void DeleteTreatment(long id)
        {
            using (context)
            {
                try
                {
                    context.Treatements.Delete(t => t.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

        #region Steps

        public List<Step> GetSteps()
        {
            List<Step> steps = new List<Step>();

            using (context)
            {
                try
                {
                    steps = context.Steps.ToList();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return steps;
            }
        }

        public Step GetStep(long id)
        {
            Step step = new Step();

            using (context)
            {
                try
                {
                    step = context.Steps.FirstOrDefault(s => s.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return step;
            }
        }

        public void CreateStep(Step newStep)
        {
            using (context)
            {
                try
                {
                    context.Steps.Add(newStep);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateStep(long id, Step updatedStep)
        {
            Step step = new Step();

            using (context)
            {
                try
                {
                    step = context.Steps.FirstOrDefault(s => s.Id == id);

                    if (!step.Equals(updatedStep))
                    {
                        step.Treatements = updatedStep.Treatements;
                        step.Id = updatedStep.Id;
                        step.Name = updatedStep.Name;
                        step.Position = updatedStep.Position;

                        context.SaveChanges();
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteStep(long id)
        {
            using (context)
            {
                try
                {
                    context.Steps.Delete(s => s.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }

        }
        #endregion

        #region Subtractor

        public List<SubContractor> GetSubContractors()
        {
            List<SubContractor> subContractors = new List<SubContractor>();

            using (context)
            {
                try
                {
                    subContractors = context.SubContractors.ToList();

                }
                catch(Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return subContractors;
            }
        }

        public SubContractor GetSubContractor(long id)
        {
            SubContractor subContractor = new SubContractor();

            using (context)
            {
                try
                {
                    subContractor = context.SubContractors.FirstOrDefault(sc => sc.Id == id);
                }
                catch(Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return subContractor;
            }
        }

        public void CreateSubContractor(SubContractor newSubContractor)
        {
            using (context)
            {
                try
                {
                    context.SubContractors.Add(newSubContractor);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateSubContractor(long id, SubContractor updatedContractor)
        {
            SubContractor subContractor = new SubContractor();

            using (context)
            {
                try
                {
                    subContractor = context.SubContractors.FirstOrDefault(sc => sc.Id == id);

                    if (!subContractor.Equals(updatedContractor))
                    {
                        subContractor.Id = updatedContractor.Id;
                        subContractor.Adress = updatedContractor.Adress;
                        subContractor.Nom = updatedContractor.Nom;
                        subContractor.StepsInCharge = updatedContractor.StepsInCharge;
                        subContractor.Workers = updatedContractor.Workers;

                        context.SaveChanges();
                    }
                }
                catch(Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteSubContractor(long id)
        {
            using (context)
            {
                try
                {
                    context.SubContractors.Delete(sc => sc.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }
        #endregion

        #region Organization
        public List<Organisation> GetOrganizations()
        {
            List<Organisation> organization = new List<Organisation>();

            using (context)
            {
                try
                {
                    organization = context.Organisations.ToList();

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return organization;
            }
        }

        public Organisation GetOrganization(long id)
        {
            Organisation organization = new Organisation();

            using (context)
            {
                try
                {
                    organization = context.Organisations.FirstOrDefault(o => o.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return organization;
            }
        }

        public void CreateOrganization(Organisation newOrganization)
        {
            using (context)
            {
                try
                {
                    context.Organisations.Add(newOrganization);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateOrganization(long id, Organisation updatedOrganization)
        {
            Organisation organization = new Organisation();

            using (context)
            {
                try
                {
                    organization = context.Organisations.FirstOrDefault(o => o.Id == id);

                    if (!organization.Equals(updatedOrganization))
                    {
                        organization.Id = updatedOrganization.Id;
                        organization.Adresse = updatedOrganization.Adresse;
                        organization.Email = updatedOrganization.Email;
                        organization.Nom = updatedOrganization.Nom;
                        organization.Processes = updatedOrganization.Processes;
                        organization.SubContractors = updatedOrganization.SubContractors;
                        organization.Telephone = updatedOrganization.Telephone;
                        organization.Workers = updatedOrganization.Workers;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteOrganization(long id)
        {
            using (context)
            {
                try
                {
                    context.Organisations.Delete(o => o.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }
        #endregion

        #region Address

        public List<Adresse> GetAddress()
        {
            List<Adresse> address = new List<Adresse>();

            using (context)
            {
                try
                {
                    address = context.Adresses.ToList();

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return address;
            }
        }

        public Adresse GetAddress(long id)
        {
            Adresse address = new Adresse();

            using (context)
            {
                try
                {
                    address = context.Adresses.FirstOrDefault(o => o.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return address;
            }
        }

        public void CreateAddress(Adresse newAddress)
        {
            using (context)
            {
                try
                {
                    context.Organisations.Add(newAddress);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateAddress(long id, Adresse updatedAddress)
        {
            Adresse address = new Adresse();

            using (context)
            {
                try
                {
                    address = context.Adresses.FirstOrDefault(a => a.Id == id);

                    if (!address.Equals(updatedAddress))
                    {
                        address.Id = updatedAddress.Id;
                        address.CodePostal = updatedAddress.CodePostal;
                        address.Numero = updatedAddress.Numero;
                        address.Pays = updatedAddress.Pays;
                        address.Rue = updatedAddress.Rue;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteAddress(long id)
        {
            using (context)
            {
                try
                {
                    context.Adresses.Delete(a => a.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

        #region Process

        public List<Processus> GetProcesses()
        {
            List<Processus> processes = new List<Processus>();

            using (context)
            {
                try
                {
                    processes = context.Processes.ToList();

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return processes;
            }
        }

        public Processus GetProcess(long id)
        {
            Processus process = new Processus();

            using (context)
            {
                try
                {
                    process = context.Processes.FirstOrDefault(p => p.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return process;
            }
        }

        public void CreateProcess(Processus newProcess)
        {
            using (context)
            {
                try
                {
                    context.Processes.Add(newProcess);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateProcess(long id, Processus updatedProcess)
        {
            Processus process = new Processus();

            using (context)
            {
                try
                {
                    process = context.Processes.FirstOrDefault(p => p.Id == id);

                    if (!process.Equals(updatedProcess))
                    {
                        process.Id = updatedProcess.Id;
                        process.Description = updatedProcess.Description;
                        process.Etapes = updatedProcess.Etapes;
                        process.Nom = updatedProcess.Nom;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteProcess(long id)
        {
            using (context)
            {
                try
                {
                    context.Processes.Delete(p => p.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

        #region Process

        public List<State> GetStates()
        {
            List<State> states = new List<State>();

            using (context)
            {
                try
                {
                    states = context.States.ToList();

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return states;
            }
        }

        public State GetState(long id)
        {
            State state = new State();

            using (context)
            {
                try
                {
                    state = context.States.FirstOrDefault(s => s.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return state;
            }
        }

        public void CreateState(State newState)
        {
            using (context)
            {
                try
                {
                    context.States.Add(newState);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateState(long id, State updatedState)
        {
            State state = new State();

            using (context)
            {
                try
                {
                    state = context.States.FirstOrDefault(s => s.Id == id);

                    if (!state.Equals(updatedState))
                    {
                        state.Id = updatedState.Id;
                        state.ProductsConcerned = updatedState.ProductsConcerned;
                        state.Status = updatedState.Status;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteState(long id)
        {
            using (context)
            {
                try
                {
                    context.States.Delete(s => s.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

        #region Role

        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();

            using (context)
            {
                try
                {
                    roles = context.Roles.ToList();

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return roles;
            }
        }

        public Role GetRole(long id)
        {
            Role role = new Role();

            using (context)
            {
                try
                {
                    role = context.Roles.FirstOrDefault(r => r.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return role;
            }
        }

        public void CreateRole(Role newRole)
        {
            using (context)
            {
                try
                {
                    context.Roles.Add(newRole);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateRole(long id, Role updatedRole)
        {
            Role role = new Role();

            using (context)
            {
                try
                {
                    role = context.Roles.FirstOrDefault(r => r.Id == id);

                    if (!role.Equals(updatedRole))
                    {
                        role.Id = updatedRole.Id;
                        role.Description = updatedRole.Description;
                        role.Nom = updatedRole.Nom;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void DeleteRole(long id)
        {
            using (context)
            {
                try
                {
                    context.Roles.Delete(r => r.Id == id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

        #region Scan

        public List<Scan> GetScans()
        {
            List<Scan> scans = new List<Scan>();

            using (context)
            {
                try
                {
                    scans = context.Scans.ToList();

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return scans;
            }
        }

        public Scan GetScan(long iduser,long idTreatement)
        {
            Scan scan = new Scan();

            using (context)
            {
                try
                {
                    scan = context.Scans.FirstOrDefault(sc => sc.UserId == iduser && sc.TreatmentId == idTreatement);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return scan;
            }
        }

        public void CreateScan(Scan newScan)
        {
            using (context)
            {
                try
                {
                    context.Scans.Add(newScan);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        

        public void DeleteScan(long iduser, long idTreatement)
        {
            using (context)
            {
                try
                {
                    context.Scans.Delete(sc => sc.UserId == iduser && sc.TreatmentId == idTreatement);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

        #region ProductStateDefinition

        public List<ProductStateDefinition> GetProductStateDefinitions()
        {
            List<ProductStateDefinition> productStateDefinitions = new List<ProductStateDefinition>();

            using (context)
            {
                try
                {
                    productStateDefinitions = context.ProductStateDefinitions.ToList();

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return productStateDefinitions;
            }
        }

        public ProductStateDefinition GetProductStateDefinition(long productId, long stateid)
        {
            ProductStateDefinition productStateDefinition = new ProductStateDefinition();

            using (context)
            {
                try
                {
                    productStateDefinition = context.ProductStateDefinitions.FirstOrDefault(psd => psd.ProductId == productId && psd.Stateid == stateid);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return productStateDefinition;
            }
        }

        public void CreateProductStateDefinition(ProductStateDefinition newProductStateDefinition)
        {
            using (context)
            {
                try
                {
                    context.ProductStateDefinitions.Add(newProductStateDefinition);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateProductStateDefinition(long productId, long stateid, ProductStateDefinition updatedProductStateDefinition)
        {
            ProductStateDefinition productStateDefinition = new ProductStateDefinition();

            using (context)
            {
                try
                {
                    productStateDefinition = context.ProductStateDefinitions.FirstOrDefault(psd => psd.ProductId == productId && psd.Stateid == stateid);

                    if (!productStateDefinition.Equals(updatedProductStateDefinition))
                    {
                        productStateDefinition.ProductId = updatedProductStateDefinition.ProductId;
                        productStateDefinition.Stateid = updatedProductStateDefinition.Stateid;
                        productStateDefinition.AcquisitionDate = updatedProductStateDefinition.AcquisitionDate;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }



        public void DeleteProductStateDefinition(long productId, long stateid)
        {
            using (context)
            {
                try
                {
                    context.ProductStateDefinitions.Delete(psd => psd.ProductId == productId && psd.Stateid == stateid);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

    }
}