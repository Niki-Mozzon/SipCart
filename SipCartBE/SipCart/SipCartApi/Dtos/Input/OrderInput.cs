
using SipCartCore.Entities;

namespace SipCartApi.Dtos.Input
{
    public class OrderInput
    {
        public decimal TotalPrice { get; set; }
        public string? CouponCode { get; set; }
        public ePaymentMethod PaymentMethod { get; set; }
    }
}
