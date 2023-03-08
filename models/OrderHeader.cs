using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RedMango_API.models
{
    public class OrderHeader
    {
        [Key]
        public int OrderHeaderId { get; set; }
        [Required]
        public string PickupName { get; set; }
        [Required]
        public string PickupPhoneNumber { get; set; }
        [Required]
        public string PickupEmail { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public MongoUser User  { get; set; }
        public double OrderTotal { get; set; }


        public DateTime OrderDate { get; set; }
        public string StripePaymentIntentID { get; set; }
        public string Status { get; set; }
        public int TotalItems { get; set; }

       public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
