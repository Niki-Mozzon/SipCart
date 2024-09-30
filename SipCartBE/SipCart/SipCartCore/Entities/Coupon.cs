using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipCartCore.Entities
{
    [Table("coupons")]
    public class Coupon
    {
        [Key]
        public string Code { get; set; }
        [Required]
        [Range(0, 100)]
        public decimal PercentageReduction { get; set; }
    }
}
