using System;

namespace TracageAlmentaireWeb.Models
{
    public class Scan
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long UserId { get; set; }

        public long TreatmentId { get; set; }

        public long OutgoingStateId { get; set; }

        public DateTime DateOfScan { get; set; }

    }
}