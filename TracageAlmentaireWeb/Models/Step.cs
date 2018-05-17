using System.Collections.Generic;

namespace Tracage.Models
{
    public class Step
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public long Process_Id { get; set; }

        public List<Treatment> Treatments { get; set; }


    }
}
