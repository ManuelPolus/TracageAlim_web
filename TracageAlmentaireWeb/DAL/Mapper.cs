using System;
using System.Collections.Generic;
using System.Linq;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Components;
using TracageAlmentaireWeb.Models;
using TracageAlmentaireWeb.ViewModels;

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
            using (context = new TrackingDataContext("FTDb"))
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
            using (context = new TrackingDataContext("FTDb"))
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
            using (context = new TrackingDataContext("FTDb"))
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
                    if (!PasswordHasher.CheckPassword(vm.Password + bob.Salt, bob.Password))
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
            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {

                    PasswordHasher.Hash(bob);

                    context.Users.Add(bob);

                    bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == bob.CurrentRole_Id);
                    if (bob.CurrentRole == null)
                    {
                        bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == 1);
                        context.SaveChanges();
                    }

                    Address addressBob = new Address();
                    addressBob = context.Adresses.FirstOrDefault(a => a.Number == bob.Address.Number && a.Street == bob.Address.Street);
                    try
                    {
                        if (addressBob.Equals(null))
                        {
                            //I do not know why the context returns a non assigned object ref instead of just empty stuff... catching the exception
                            // works though...
                        }
                        else
                        {
                            bob.AddressId = addressBob.Id;
                        }
                    }
                    catch (Exception e)
                    {
                        context.Adresses.Add(bob.Address);
                        context.SaveChanges();
                        bob.AddressId = context.Adresses.FirstOrDefault(a => a.Number == bob.Address.Number && a.Street == bob.Address.Street).Id;
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
                        PasswordHasher.Hash(newBob);
                        bob.Password = newBob.Password;
                        bob.Salt = newBob.Salt;
                        bob.Telephone = newBob.Telephone;
                        bob.CurrentRole_Id = newBob.CurrentRole_Id;
                        bob.CurrentRole = context.Roles.FirstOrDefault(r => r.Id == newBob.CurrentRole_Id);

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
            using (context = new TrackingDataContext("FTDb"))
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

                    product.CurrentTreatment = context.Treatements.FirstOrDefault(t => t.Id == product.CurrrentTreatmentId);
                    List<ProductStateDefinition> psds = context.ProductStateDefinitions
                        .Where(p => p.ProductId == product.Id).ToList();
                    foreach (var psd in psds)
                    {
                        product.States.Add(context.States.FirstOrDefault(s => s.Id == psd.StateId));
                    }
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
                    product.States = new List<State>();
                    List<ProductStateDefinition> psds = context.ProductStateDefinitions
                        .Where(p => p.ProductId == product.Id).ToList();
                    foreach (var psd in psds)
                    {
                        product.States.Add(context.States.FirstOrDefault(s => s.Id == psd.StateId));
                    }
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
                    if (newProduct.CurrentTreatment != null)
                    {
                        newProduct.CurrrentTreatmentId = newProduct.CurrentTreatment.Id;
                    }
                    else

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

                        try
                        {
                            if (product.Process == null)
                            {
                                Process process =
                                    context.Processes.FirstOrDefault(p => p.Id == updatedProduct.ProcessId);
                                process.Steps = context.Steps.Where(s => s.Process_Id == process.Id).ToList();
                                foreach (var step in process.Steps)
                                {
                                    step.Treatments = context.Treatements.Where(t => t.StepId == step.Id).ToList();
                                }

                                updatedProduct.CurrrentTreatmentId = process.Steps.ElementAt(0).Treatments.ElementAt(0).Id;
                                product.ProcessId = updatedProduct.ProcessId;
                            }

                            product.CurrentTreatment = context.Treatements.FirstOrDefault(t => t.Id == updatedProduct.CurrentTreatment.Id);
                            product.CurrentTreatment.OutgoingState = context.States.FirstOrDefault(s => s.Id == product.CurrentTreatment.OutgoingStateId);
                            product.CurrrentTreatmentId = product.CurrentTreatment.Id;
                            product.States = updatedProduct.States;
                            foreach (var productState in product.States)
                            {
                                if (context.ProductStateDefinitions.FirstOrDefault(
                                        psd => psd.StateId == productState.Id && psd.ProductId == product.Id) == null)
                                {
                                    ProductStateDefinition psd = new ProductStateDefinition(productState.Id, product.Id);
                                    context.ProductStateDefinitions.Add(psd);
                                }
                            }
                        }
                        catch (NullReferenceException e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }

                    }

                    product.Description = updatedProduct.Description;
                    product.Name = updatedProduct.Name;

                    context.SaveChanges();
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
                        try
                        {
                            if (product.ProcessId == 0 || product.ProcessId == null)
                            {
                                Process process =
                                    context.Processes.FirstOrDefault(p => p.Id == updatedProduct.ProcessId);
                                process.Steps = context.Steps.Where(s => s.Process_Id == process.Id).ToList();
                                foreach (var step in process.Steps)
                                {
                                    step.Treatments = context.Treatements.Where(t => t.StepId == step.Id).ToList();
                                }


                                product.ProcessId = updatedProduct.ProcessId;

                                product.CurrentTreatment = new Treatment();
                                product.CurrentTreatment = process.Steps.ElementAt(0).Treatments.ElementAt(0);
                                product.CurrentTreatment.OutgoingState = context.States.FirstOrDefault(s => s.Id == product.CurrentTreatment.OutgoingStateId);
                                product.CurrrentTreatmentId = process.Steps.ElementAt(0).Treatments.ElementAt(0).Id;
                                product.States = new List<State>();
                                //product.States.Add(product.CurrentTreatment.OutgoingState);
                                context.ProductStateDefinitions.Add(new ProductStateDefinition(product.CurrentTreatment.OutgoingState.Id, product.Id));
                                context.SaveChanges();
                            }
                            else
                            {
                                product.CurrentTreatment = new Treatment();
                                product.CurrentTreatment = context.Treatements.FirstOrDefault(t => t.Id == updatedProduct.CurrrentTreatmentId);
                                product.CurrentTreatment.OutgoingState = context.States.FirstOrDefault(s => s.Id == product.CurrentTreatment.OutgoingStateId);
                                product.CurrrentTreatmentId = product.CurrentTreatment.Id;

                                foreach (var productState in updatedProduct.States)
                                {
                                    ProductStateDefinition psdef = context.ProductStateDefinitions.FirstOrDefault(psd => psd.StateId == productState.Id && psd.ProductId == product.Id);

                                    if (psdef == null)
                                    {
                                        context.ProductStateDefinitions.Add(new ProductStateDefinition(productState.Id, product.Id));
                                    }
                                }
                            }

                        }
                        catch (NullReferenceException e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }

                    }

                    product.Description = updatedProduct.Description;
                    product.Name = updatedProduct.Name;


                    context.SaveChanges();
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

            using (context = new TrackingDataContext("FTDb"))
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


        public List<Treatment> GetTreatmentsByStep(long stepId)
        {
            List<Treatment> treatments = new List<Treatment>();

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    treatments = context.Treatements.Where(t => t.StepId == stepId).ToList();

                    foreach (Treatment treatment in treatments)
                        treatment.OutgoingState = context.States.FirstOrDefault(s => s.Id == treatment.OutgoingStateId);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return treatments;
            }

        }

        public Treatment GetTreatment(long id)
        {
            Treatment treatment = new Treatment();

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    treatment = context.Treatements.FirstOrDefault(t => t.Id == id);
                    treatment.OutgoingState = context.States.FirstOrDefault(s => s.Id == treatment.OutgoingStateId);
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

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    Role rl = context.Roles.FirstOrDefault(r => r.Name == "Administrator");
                    context.UserScanRights.Add(new UserScanRights { RoleId = rl.Id,TreatmentId = newTreatment.Id});
                    context.Treatements.Add(newTreatment);
                    context.SaveChanges();
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

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    treatment = context.Treatements.FirstOrDefault(t => t.Id == id);

                    if (!treatment.Equals(updatedTreatment))
                    {
                        treatment.StepId = updatedTreatment.StepId;
                        treatment.Id = updatedTreatment.Id;
                        treatment.Description = updatedTreatment.Description;
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
            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    Treatment treatmentToDelete = context.Treatements.FirstOrDefault(t => t.Id == id);
                    context.Treatements.Remove(treatmentToDelete);
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

            using (context = new TrackingDataContext("FTDb"))
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

            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    step = context.Steps.FirstOrDefault(s => s.Id == id);
                    step.Treatments = context.Treatements.Where(t => t.StepId == step.Id).ToList();
                    foreach (var t in step.Treatments)
                    {
                        t.OutgoingState = context.States.FirstOrDefault(s => s.Id == t.OutgoingStateId);
                    }
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
            using (context = new TrackingDataContext("FTDb"))
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
                        step.Process_Id = updatedStep.Process_Id;

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
            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    Step stepToDelete = context.Steps.FirstOrDefault(s => s.Id == id);
                    context.Steps.Remove(stepToDelete);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }

        }
        #endregion

        #region SubContractor

        public List<SubContractor> GetSubContractors()
        {
            List<SubContractor> subContractors = new List<SubContractor>();

            using (context = new TrackingDataContext("FTDb"))
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
                    SubContractor subToDelete = context.SubContractors.FirstOrDefault(sub => sub.Id == id);
                    context.SubContractors.Remove(subToDelete);
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
                    Organization orgaToDelete = context.Organisations.FirstOrDefault(o => o.Id == id);
                    context.Organisations.Remove(orgaToDelete);
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
                    Address addToDelete = context.Adresses.FirstOrDefault(a => a.Id == id);
                    context.Adresses.Remove(addToDelete);
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
                        foreach (var t in step.Treatments)
                        {
                            t.OutgoingState = context.States.FirstOrDefault(s => s.Id == t.OutgoingStateId);
                        }
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
                        process.Name = updatedProcess.Name;
                        process.Description = updatedProcess.Description;

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
                    Process proToDelete = context.Processes.FirstOrDefault(p => p.Id == id);
                    context.Processes.Remove(proToDelete);
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
                    State proToDelete = context.States.FirstOrDefault(s => s.Id == id);
                    context.States.Remove(proToDelete);
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

            using (context = new TrackingDataContext("FTDb"))
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

            using (context = new TrackingDataContext("FTDb"))
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

            using (context = new TrackingDataContext("FTDb"))
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
            using (context = new TrackingDataContext("FTDb"))
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

            using (context = new TrackingDataContext("FTDb"))
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
            using (context = new TrackingDataContext("FTDb"))
            {
                try
                {
                    Role roleToDelete = context.Roles.FirstOrDefault(r => r.Id == id);
                    context.Roles.Remove(roleToDelete);
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

        public Scan GetScan(long iduser, long idTreatement, long idProduct)
        {
            Scan scan = new Scan();

            using (context)
            {
                try
                {
                    scan = context.Scans.FirstOrDefault(sc => sc.UserId == iduser && sc.TreatmentId == idTreatement && sc.ProductId == idProduct);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return scan;
            }
        }

        public List<Scan> GetScanByUser(long iduser)
        {
            List<Scan> scans = new List<Scan>();

            using (context)
            {
                try
                {
                    scans = context.Scans.Where(sc => sc.UserId == iduser).ToList();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return scans;
            }
        }

        public List<Scan> GetScanByTreatment(long idTreatment)
        {
            List<Scan> scans = new List<Scan>();

            using (context)
            {
                try
                {
                    scans = context.Scans.Where(sc => sc.TreatmentId == idTreatment).ToList();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return scans;
            }
        }

        public List<Scan> GetScanByProduct(long idProduct)
        {
            List<Scan> scans = new List<Scan>();

            using (context)
            {
                try
                {
                    scans = context.Scans.Where(sc => sc.ProductId == idProduct).ToList();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }

                return scans;
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
                    Scan scanToDelete = context.Scans.FirstOrDefault(s => s.UserId == iduser && s.TreatmentId == idTreatement);
                    context.Scans.Remove(scanToDelete);
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
                    ProductStateDefinition proStateDefToDelete = context.ProductStateDefinitions.FirstOrDefault(psd => psd.ProductId == productId && psd.StateId == stateid);
                    context.ProductStateDefinitions.Remove(proStateDefToDelete);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion

        #region Rights

        public List<UserScanRights> GetRightsByRole(long roleId)
        {
            try
            {
                List<UserScanRights> usr = new List<UserScanRights>();

                usr = context.UserScanRights.Where(us => us.RoleId == roleId).ToList();

                return usr;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public void CreateUserScanRights(long roleId,long treatmentId)
        {
            context.UserScanRights.Add(new UserScanRights {RoleId = roleId, TreatmentId = treatmentId});
        }

        #endregion
    }
}