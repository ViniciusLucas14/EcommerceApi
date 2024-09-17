using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Models
{
    public sealed class Products
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
    public class ProductsEntityTypeConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder
                .Property(b => b.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder
              .Property(b => b.Price)
              .IsRequired();
        }
    }
}
