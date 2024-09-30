using Microsoft.AspNetCore.Mvc;
using SipCartCore.Entities;
using SipCartCore.Exceptions;
using SipCartCore.Services.Interfaces;

namespace SipCartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponController
        (ILogger<CouponController> logger, ICouponService couponService) : ControllerBase
    {
        private readonly ICouponService _couponService = couponService;
        private readonly ILogger<CouponController> _logger = logger;

        [HttpGet("all", Name = "GetAllCoupons")]
        public async Task<ActionResult<IEnumerable<Drink>>> GetAllCoupons()
        {
            try
            {
                IEnumerable<Coupon> coupons = await _couponService.GetAllCouponsAsync();
                return Ok(coupons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("coupon", Name = "GetCouponByCode")]
        public async Task<ActionResult<Drink>> GetCouponById([FromQuery] string code)
        {
            try
            {
                return Ok(await _couponService.GetCouponByCodeAsync(code));

            }
            catch (Exception ex)
            {
                if (ex is CouponNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return BadRequest();
            }
        }

    }
}

