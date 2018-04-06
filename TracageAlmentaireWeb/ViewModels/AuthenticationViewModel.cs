using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace TracageAlmentaireWeb.ViewModels
{
    public class AuthenticationViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}