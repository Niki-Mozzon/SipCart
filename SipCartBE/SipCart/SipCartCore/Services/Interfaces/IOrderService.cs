using SipCartCore.Entities;

namespace SipCartCore.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<int?> AddOrderAsync(decimal totalPrice, string? couponCode, ePaymentMethod paymentMethod);
        public Task<OrderDetail> CheckOutAndCreateOrderAsync(Dictionary<int, int> itemsIds, string? couponCode);
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}