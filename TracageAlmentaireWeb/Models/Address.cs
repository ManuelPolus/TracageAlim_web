using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class Address
    {
        public long Id { get; set; }
        public string Street { get; set; }

        public string Number { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }


    }
}
