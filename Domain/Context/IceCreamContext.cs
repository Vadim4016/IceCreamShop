using System.Data.Entity;
using Domain.Model;

namespace Domain.Context
{
    public class IceCreamContext : DbContext
    {
        public DbSet<IceCream> IceCreams { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
