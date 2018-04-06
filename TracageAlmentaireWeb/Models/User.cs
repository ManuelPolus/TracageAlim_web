using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class User
    {
    

        public int Id { get; set;  }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Telephone { get; set; }

        public long CurrentRole_Id { get; set; }

        public string Salt { get; set; }

        public Address Address { get; set; }

        public Role CurrentRole { get; set; }
    }
}
