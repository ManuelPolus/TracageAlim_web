using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tracage.Models
{
    public class User
    {
    

        public int Id { get; set;  }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Telephone { get; set; }

        public long CurrentRole_Id { get; set; }

        public string Salt { get; set; }

        public Address Address { get; set; }

        public Role CurrentRole { get; set; }
    }
}
