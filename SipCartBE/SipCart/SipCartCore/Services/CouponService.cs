using Microsoft.EntityFrameworkCore;
using SipCartCore.Entities;
using SipCartCore.Exceptions;
using SipCartCore.Services.Interfaces;

namespace SipCartCore.Services
{
    public class CouponService(AppContext context) : ICouponService
    {
        private readonly AppContext _context = context;

        public async Task<IEnumerable<Coupon>> GetAllCouponsAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetCouponByCodeAsync(string couponCode)
        {

            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(d => d.Code == couponCode);
            if (coupon == null)
            {
                throw new CouponNotFoundException("Coupon code not valid!");
            }
            return coupon;
        }

    }
}
