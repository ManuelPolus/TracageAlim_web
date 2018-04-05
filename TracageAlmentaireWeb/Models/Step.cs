using System;
using System.Collections.Generic;
using System.Text;

namespace Tracage.Models
{
    public class Step
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public List<Treatment> Treatments { get; set; }


    }
}
