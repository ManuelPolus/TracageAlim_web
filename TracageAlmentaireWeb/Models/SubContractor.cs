using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class SubContractor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public List<Step> StepsInCharge { get; set; }

        public List<User> Workers { get; set; }
    }
}
