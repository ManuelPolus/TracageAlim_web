using DateTime = System.DateTime;

namespace TracageAlmentaireWeb.Models
{
    public class ProductStateDefinition
    {
        public ProductStateDefinition()
        {

        }
        public ProductStateDefinition(long stateId, long productId)
        {
            this.StateId = stateId;
            this.ProductId = productId;
            AcquisitionDate = DateTime.Now;
        }
        public long ProductId { get; set; }

        public long StateId { get; set; }

        public DateTime AcquisitionDate { get; set; }


    }
}