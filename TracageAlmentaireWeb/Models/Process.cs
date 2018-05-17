using System.Collections.Generic;

namespace Tracage.Models
{
    public class Process
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Step> Steps { get; set; }

        public string Description { get; set; }
    }
}
