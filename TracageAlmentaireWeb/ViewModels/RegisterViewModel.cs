using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.ViewModels
{
    public class RegisterViewModel
    {

        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string PasswordCheck { get; set; }

        
        public string StreetName { get; set; }

        [MaxLength(5)]
        public string Number { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string Country { get; set; }

        public bool CheckPasswords()
        {
            return Password == PasswordCheck;
        }

    }
}