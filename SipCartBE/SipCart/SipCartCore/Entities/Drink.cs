using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipCartCore.Entities
{
    [Table("drinks")]
    public class Drink
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
