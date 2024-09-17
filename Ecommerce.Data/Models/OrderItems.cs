using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ecommerce.Data.Models
{

    public sealed class OrderItems
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public int OrdersId { get; set; }
        public int ProductsId { get; set; }
        public Orders Orders { get; set; }
        public Products Products { get; set; }
    }
    public class OrderItemsEntityTypeConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.Property(b => b.Quantity).IsRequired();
            builder.Property(b => b.Total).IsRequired();
            builder.Property(b => b.Subtotal).IsRequired();
            builder.Property(b => b.Discount).IsRequired();
        }
    }
}
