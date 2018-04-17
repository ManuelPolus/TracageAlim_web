using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracage.Models;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.ViewModels;

namespace TracageAlmentaireWeb.BL
{
    public class ControllerDBRequester
    {

        private static Mapper mapper = new Mapper("FTDb");

        public static bool Register(RegisterViewModel userToRegister)
        {
            try
            {
                User u = new User();

                u.Name = userToRegister.FullName;
                u.Email = userToRegister.Email;
                u.Password = userToRegister.Password;
                u.Telephone = userToRegister.PhoneNumber;
                u.Address = new Address(
                    userToRegister.StreetName,
                    userToRegister.Number,
                    userToRegister.PostalCode,
                    userToRegister.Country
                );
                if (userToRegister.CheckPasswords())
                {
                    mapper.CreateUser(u);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

    }
}