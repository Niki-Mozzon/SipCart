using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipCartCore.Entities
{
    public class OrderDetail : Order
    {
        [NotMapped]
        [JsonProperty("Cart", NullValueHandling = NullValueHandling.Ignore)]
        public List<Product>? Cart { get; set; }
        [NotMapped]
        [JsonProperty("FullPrice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? FullPrice { get; set; }


    }
}
