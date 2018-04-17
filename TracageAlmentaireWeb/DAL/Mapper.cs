using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Http.ModelBinding;
using LinqToDB;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Components;
using TracageAlmentaireWeb.Models;
using TracageAlmentaireWeb.ViewModels;
using ModelState = System.Web.Mvc.ModelState;

namespace TracageAlmentaireWeb.DAL
{

    
    public class Mapper
    {
        private string database;

        private TrackingDataContext context;

        //TODO: Tokenize the rest service
        public Mapper(string database)
        {
            this.database = database;
            this.context = new TrackingDataContext(database);
        }

        #region Users

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (context)
            {
                try
                {


                    users = context.Users.ToList();
                    foreach (var bob in users)
                    {
                        bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == bob.CurrentRole_Id);
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
                return users;
            }
        }

        public virtual User GetUser(long id)
        {
            User bob = new User();
            using (context)
            {
                try
                {

                    bob = context.Users.FirstOrDefault(u => u.Id == id);
                    bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == bob.CurrentRole_Id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
                return bob;
            }
        }

        public virtual User GetUser(string email)
        {
            User bob = new User();
            using (context)
            {
                try
                {

                    bob = context.Users.FirstOrDefault(u => u.Email == email);
                    bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == bob.CurrentRole_Id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
                return bob;
            }
        }

        public virtual User AthentifyUser(AuthenticationViewModel vm)
        {
            User bob = new User();

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {

                    bob = context.Users.FirstOrDefault(u => u.Email == vm.Email);
                    if (! PasswordHasher.CheckPassword(vm.Password + bob.Salt, bob.Password))
                    {
                        return null;
                    }


                    bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == bob.CurrentRole_Id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
                return bob;
            }
        }

        public void CreateUser(User bob)
        {
            using (context)
            {
                try
                {

                    PasswordHasher.Hash(bob);
                 
                    context.Users.Add(bob);

                    bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == bob.CurrentRole_Id);
                    if (bob.CurrentRole == null)
                    {
                        bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == 1);
                    }
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

            }
        }

        public void UpdateUser(long id, User newBob)
        {
            User bob = new User();
            using (context = new TrackingDataContext(database))
            {
                try
                {
                    bob = context.Users.FirstOrDefault(u => u.Id == id);

                    if (!bob.Equals(newBob))
                    {

                        bob.Name = newBob.Name;
                        bob.Email = newBob.Email;
                        bob.Address = newBob.Address;
                        bob.Password = newBob.Password;
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
                    User userToDelete = context.Users.FirstOrDefault(u => u.Id == id); 
                    context.Users.Remove(userToDelete);
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

            using (context = new TrackingDataContext("FTDb"))
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

        public virtual Product GetProductById(long id)
        {
            Product product = new Product();
            using (context = new TrackingDataContext("FTDb"))
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

        public virtual Product GetProductByQr(string qrCode)
        {
            Product product = new Product();
            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    product = context.Products.FirstOrDefault(p => p.QRCode == qrCode);
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
            using (context = new TrackingDataContext("FTDb"))
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

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    product = context.Products.FirstOrDefault(p => p.Id == id);

                    if (!product.Equals(updatedProduct))
                    {
                        product.Id = updatedProduct.Id;
                        product.CurrentTreatment = updatedProduct.CurrentTreatment;
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

        public void UpdateProduct(string qr, Product updatedProduct)
        {
            Product product = new Product();

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    product = context.Products.FirstOrDefault(p => p.QRCode == qr);

                    if (!product.Equals(updatedProduct))
                    {
                        product.Id = updatedProduct.Id;
                        product.CurrentTreatment = updatedProduct.CurrentTreatment;
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
            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    Product productToDelete = context.Products.FirstOrDefault(p => p.Id == id);
                    context.Products.Remove(productToDelete);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }

        }
        #endregion

        #region Treatments

        public List<Treatment> GetTreatments()
        {
            List<Treatment> treatements = new List<Treatment>();

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

        public Treatment GetTreatment(long id)
        {
            Treatment treatment = new Treatment();

            using (context)
            {
                try
                {
                    treatment = context.Treatements.FirstOrDefault(t => t.Id == id);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return treatment;
            }

        }

        public void CreateTreatment(Treatment newTreatment)
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

        public void UpdateTreatment(long id, Treatment updatedTreatment)
        {
            Treatment treatment = new Treatment();

            using (context)
            {
                try
                {
                    treatment = context.Treatements.FirstOrDefault(t => t.Id == id);

                    if (!treatment.Equals(updatedTreatment))
                    {
                        treatment.Id = updatedTreatment.Id;
                        treatment.Desrciption = updatedTreatment.Desrciption;
                        treatment.Name = updatedTreatment.Name;
                        treatment.OutgoingState = updatedTreatment.OutgoingState;
                        treatment.Position = updatedTreatment.Position;

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
                        step.Treatments = updatedStep.Treatments;
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
                catch (Exception e)
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
                catch (Exception e)
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
                        subContractor.Address = updatedContractor.Address;
                        subContractor.Name = updatedContractor.Name;
                        subContractor.StepsInCharge = updatedContractor.StepsInCharge;
                        subContractor.Workers = updatedContractor.Workers;

                        context.SaveChanges();
                    }
                }
                catch (Exception e)
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
        public List<Organization> GetOrganizations()
        {
            List<Organization> organization = new List<Organization>();

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

        public Organization GetOrganization(long id)
        {
            Organization organization = new Organization();

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

        public void CreateOrganization(Organization newOrganization)
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

        public void UpdateOrganization(long id, Organization updatedOrganization)
        {
            Organization organization = new Organization();

            using (context)
            {
                try
                {
                    organization = context.Organisations.FirstOrDefault(o => o.Id == id);

                    if (!organization.Equals(updatedOrganization))
                    {
                        organization.Id = updatedOrganization.Id;
                        organization.Address = updatedOrganization.Address;
                        organization.Email = updatedOrganization.Email;
                        organization.Name = updatedOrganization.Name;
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

        public List<Address> GetAddresses()
        {
            List<Address> address = new List<Address>();

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

        public Address GetAddress(long id)
        {
            Address address = new Address();

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

        public void CreateAddress(Address newAddress)
        {
            using (context)
            {
                try
                {
                    context.Adresses.Add(newAddress);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        public void UpdateAddress(long id, Address updatedAddress)
        {
            Address address = new Address();

            using (context)
            {
                try
                {
                    address = context.Adresses.FirstOrDefault(a => a.Id == id);

                    if (!address.Equals(updatedAddress))
                    {
                        address.Id = updatedAddress.Id;
                        address.PostalCode = updatedAddress.PostalCode;
                        address.Number = updatedAddress.Number;
                        address.Country = updatedAddress.Country;
                        address.Street = updatedAddress.Street;

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

        public List<Process> GetProcesses()
        {
            List<Process> processes = new List<Process>();

            using (context = new TrackingDataContext("FTDb"))
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

        public Process GetProcess(long id)
        {
            Process process = new Process();

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    process = context.Processes.FirstOrDefault(p => p.Id == id);
                    List<Step> processSteps = new List<Step>();
                    processSteps = context.Steps.Where(s => s.Process_Id == process.Id).ToList();
                    process.Steps = processSteps;
                    foreach (var step in process.Steps)
                    {
                        step.Treatments = context.Treatements.Where(t => t.StepId == step.Id).ToList();
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return process;
            }
        }

        public void CreateProcess(Process newProcess)
        {
            using (context = new TrackingDataContext("FTDb"))
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

        public void UpdateProcess(long id, Process updatedProcess)
        {
            Process process = new Process();

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    process = context.Processes.FirstOrDefault(p => p.Id == id);

                    if (!process.Equals(updatedProcess))
                    {
                        process.Id = updatedProcess.Id;
                        process.Description = updatedProcess.Description;
                        process.Steps = updatedProcess.Steps;
                        process.Name = updatedProcess.Name;

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
            using (context = new TrackingDataContext("FTDb"))
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

        #region State

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

        public Role GetRole(string name)
        {
            Role role = new Role();

            using (context)
            {
                try
                {
                    role = context.Roles.FirstOrDefault(r => r.Name == name);
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
                        role.Name = updatedRole.Name;

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

        public Scan GetScan(long iduser, long idTreatement)
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
                    productStateDefinition = context.ProductStateDefinitions.FirstOrDefault(psd => psd.ProductId == productId && psd.StateId == stateid);
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
                    productStateDefinition = context.ProductStateDefinitions.FirstOrDefault(psd => psd.ProductId == productId && psd.StateId == stateid);

                    if (!productStateDefinition.Equals(updatedProductStateDefinition))
                    {
                        productStateDefinition.ProductId = updatedProductStateDefinition.ProductId;
                        productStateDefinition.StateId = updatedProductStateDefinition.StateId;
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
                    context.ProductStateDefinitions.Delete(psd => psd.ProductId == productId && psd.StateId == stateid);
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