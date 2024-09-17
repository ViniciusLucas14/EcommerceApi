using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Models
{
    public sealed class Customers
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Category { get; set; }
        public List<Orders> Orders { get; set; }
    }
    public class CustomersEntityTypeConfiguration : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> builder)
        {
            builder
                .Property(b => b.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
              .Property(b => b.CPF)
              .HasMaxLength(11)
              .IsRequired();

            builder
              .Property(b => b.Category)
              .HasMaxLength(20)
              .IsRequired();
        }
    }
}
