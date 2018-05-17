using System.Collections.Generic;

namespace Tracage.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public Address Address { get; set; }

        public List<SubContractor> SubContractors { get; set; }

        public List<User> Workers { get; set; }

        public List<Process> Processes { get; set; }


    }
}
