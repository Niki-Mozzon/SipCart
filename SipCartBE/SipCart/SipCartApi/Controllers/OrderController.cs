using Microsoft.AspNetCore.Mvc;
using SipCartApi.Dtos.Input;
using SipCartApi.Dtos.Output;
using SipCartApi.Transformers;
using SipCartCore.Entities;
using SipCartCore.Services.Interfaces;

namespace SipCartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController(ILogger<OrderController> logger, IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;
        private readonly ILogger<OrderController> _logger = logger;

        [HttpPost("checkout", Name = "Checkout")]
        public async Task<ActionResult<OrderOutput>> CheckOut([FromBody] CheckoutInput input)
        {
            try
            {
                OrderDetail order = await _orderService.CheckOutAndCreateOrderAsync(input.Products, input.CouponCode);

                return Ok(order.OrderToOutput());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("purchase", Name = "Purchase")]
        public async Task<ActionResult<int?>> Purchase([FromBody] OrderInput order)
        {
            try
            {
                return Ok(await _orderService.AddOrderAsync(order.TotalPrice, order.CouponCode, order.PaymentMethod));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all", Name = "GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderOutput>>> GetAllOrders()
        {
            try
            {
                IEnumerable<Order> orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
