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

        public List<SubContractor> SubContractors()
        {
            List<SubContractor> subContractors = new List<SubContractor>();

            using (context)
            {
                try
                {
                    subContractors = context.SubContractors.ToList();

                }
                catch
                {

                }
            }
        }
        #endregion



    }
}