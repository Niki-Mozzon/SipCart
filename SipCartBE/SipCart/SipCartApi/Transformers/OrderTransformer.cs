using SipCartApi.Dtos.Output;
using SipCartCore.Entities;

namespace SipCartApi.Transformers
{
    public static class OrderTransformer
    {
        public static OrderOutput OrderToOutput(this OrderDetail order)
        {
            OrderOutput output = new OrderOutput
            {
                Id = order.Id ?? null,
                TotalPrice = order.TotalPrice,
                CouponCode = order.Coupon?.Code,
                PercentageReduction = order.Coupon?.PercentageReduction,
                Products = order.Cart,
                FullPrice = order.FullPrice
            };
            return output;
        }
    }
}
