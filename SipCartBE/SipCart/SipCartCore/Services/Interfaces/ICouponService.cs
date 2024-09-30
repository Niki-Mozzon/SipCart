using SipCartCore.Entities;

namespace SipCartCore.Services.Interfaces
{
    public interface ICouponService
    {
        Task<IEnumerable<Coupon>> GetAllCouponsAsync();
        Task<Coupon> GetCouponByCodeAsync(string disocuntCode);
    }
}