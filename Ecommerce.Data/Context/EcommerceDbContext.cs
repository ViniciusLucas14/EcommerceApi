using Ecommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Context
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Orders>().ToTable("orderss");
            //modelBuilder.ApplyConfiguration<Orders>(new OrdersEntityTypeConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceDbContext).Assembly);
        }
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) {
            Orders = Set<Orders>();
            Customers = Set<Customers>();
            Products = Set<Products>();
            OrderItems = Set<OrderItems>();
        }
    }
}
