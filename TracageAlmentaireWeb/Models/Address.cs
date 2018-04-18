using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class Address
    {
        public Address()
        {

        }

        public Address(string street,string number,string postalCode,string country)
        {
            this.Street = street;
            this.Number = number;
            this.PostalCode = postalCode;
            this.Country = country;
        }


        public long Id { get; set; }
        public string Street { get; set; }

        public string Number { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public static bool operator ==(Address o, Address o2)
        {
            if (o.Number == o2.Number && o.Street == o2.Street && o.PostalCode == o2.PostalCode && o.Country == o2.Country)
                return true;

            return false;
        }

        public static bool operator !=(Address o, Address o2) 
        {
            return !(o == o2);

        }

    }
}
