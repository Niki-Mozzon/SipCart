using SipCartCore.Entities;

namespace SipCartApi.Dtos.Output
{
    public class OrderOutput
    {
        public int? Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? FullPrice { get; set; }
        public string? CouponCode { get; set; }
        public decimal? PercentageReduction { get; set; }

        public List<Product> Products { get; set; }
    }
}