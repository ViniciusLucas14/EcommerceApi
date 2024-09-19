using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Models
{
    public sealed class Orders
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid Guid { get; set; }
        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public int CustomersId { get; set; }
        public Customers Customers { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
    public class OrdersEntityTypeConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.Property(b => b.Guid).IsRequired();
            builder.Property(b => b.Subtotal).IsRequired();
            builder.Property(b => b.Total).IsRequired();
            builder.Property(b => b.Discount).IsRequired();
        }
    }
}
