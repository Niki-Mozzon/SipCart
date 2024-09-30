using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SipCartCore.Entities;
using SipCartCore.Exceptions;
using SipCartCore.Services.Interfaces;

namespace SipCartCore.Services
{
    public class OrderService(AppContext context,
        ICouponService couponService,
        IDrinkService drinkService)
        : IOrderService
    {

        private readonly AppContext _context = context;
        private readonly ICouponService _couponService = couponService;
        private readonly IDrinkService _drinkService = drinkService;

        public async Task<int?> AddOrderAsync(decimal totalPrice, string? couponCode, ePaymentMethod paymentMethod)
        {
            Order order = new()
            {
                TotalPrice = totalPrice,
                CouponCode = couponCode,
                PaymentMethod = paymentMethod.ToString()
            };
            if (paymentMethod.Equals(ePaymentMethod.CASH) && totalPrice > 10)
            {
                throw new Exception("The total price is too high for cash payment, please use another payment method.");
            }
            EntityEntry<Order> entityEntry = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return entityEntry.Entity.Id;
        }

        public async Task<OrderDetail> CheckOutAndCreateOrder(Dictionary<int, int> items, string couponCode)
        {
            if (items.Count < 1)
            {
                throw new EmptyCartException("The cart is empty, add some items to check out!");
            }
            IEnumerable<int> ids = items.Keys;
            List<Drink> drinks = (await _drinkService.GetMultipleDrinksByIdAsync(ids)).ToList();
            List<Product> products = new();
            drinks.ForEach(drink =>
            {
                products.Add(new Product
                {
                    Drink = drink,
                    Quantity = items[drink.Id]
                });
            });
            Coupon? coupon = null;
            if (!string.IsNullOrEmpty(couponCode))
            {
                coupon = await _couponService.GetCouponByCodeAsync(couponCode);
            }

            OrderDetail order = new()
            {
                Cart = products
            };
            order.Coupon = coupon;
            order.CouponCode = couponCode;
            (order.TotalPrice, order.FullPrice) = GetTotalPrice(products, coupon);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        private Tuple<decimal, decimal> GetTotalPrice(List<Product> products, Coupon? coupon)
        {
            decimal totalPrice = 0;
            totalPrice += products.Sum(p => p.Drink.Price * p.Quantity);
            if (coupon != null)
            {
                decimal discounted = totalPrice - (totalPrice * ((decimal)coupon.PercentageReduction / 100));

                return Tuple.Create(Math.Round(discounted, 2), totalPrice);
            }
            return Tuple.Create(totalPrice, totalPrice);
        }
    }
}
