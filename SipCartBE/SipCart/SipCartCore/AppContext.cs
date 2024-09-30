using Microsoft.EntityFrameworkCore;
using SipCartCore.Entities;
namespace SipCartCore
{
    public class AppContext(DbContextOptions<AppContext> options) : DbContext(options)
    {
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
