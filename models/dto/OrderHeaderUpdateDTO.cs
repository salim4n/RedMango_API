namespace RedMango_API.models.dto
{
    public class OrderHeaderUpdateDTO
    {
        public int OrderHeaderId { get; set; }
        public string PickupName { get; set; }
        public string PickupPhoneNumber { get; set; }
        public string PickupEmail { get; set; }

        public string StripePaymentIntentID { get; set; }
        public string Status { get; set; }
    }
}
