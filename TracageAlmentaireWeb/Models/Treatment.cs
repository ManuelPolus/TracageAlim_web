using System;
using System.Collections.Generic;
using System.Text;
using TracageAlmentaireWeb.Models;

namespace Tracage.Models
{
    public class Treatment
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public State OutgoingState { get; set; }

        public long OutgoingStateId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }
        public long StepId { get; set; }
    }
}
