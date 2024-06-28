using Microsoft.EntityFrameworkCore;

using IMSDomain;

namespace IMSDataAccess
{
    public class ProductDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
    }
}
