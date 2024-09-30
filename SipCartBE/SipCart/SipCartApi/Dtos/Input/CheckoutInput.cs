namespace SipCartApi.Dtos.Input
{
    public class CheckoutInput
    {
        public Dictionary<int, int> Products { get; set; }
        public string? CouponCode { get; set; }
    }
}
