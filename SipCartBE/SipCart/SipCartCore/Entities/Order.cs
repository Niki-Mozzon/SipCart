using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipCartCore.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [ForeignKey("Coupon")]
        public string? CouponCode { get; set; }
        [JsonProperty("Coupon", NullValueHandling = NullValueHandling.Ignore)]
        public Coupon? Coupon { get; set; }
        public string PaymentMethod { get; set; }


    }
}
