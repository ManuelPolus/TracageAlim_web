using System;
using System.Collections.Generic;
using System.Linq;
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

                bobs = context.Users.ToList();
                return bobs;
            }
        }

        public virtual Utilisateur GetUser(long id)
        {
            Utilisateur bob = new Utilisateur();
            using (context)
            {

                bob = context.Users.FirstOrDefault(u => u.Id == id);
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

        public void UpdateUser(long id,Utilisateur newBob)
        {
            using (context)
            {
                try
                {
                    Utilisateur bob = context.Users.FirstOrDefault(u => u.Id == id);

                    if (!bob.Equals(newBob))
                    {
                        bob = newBob;
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
                    context.Users.Delete(u=> u.Id ==id);
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




        #endregion
    }
}