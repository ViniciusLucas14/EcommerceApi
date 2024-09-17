using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<EcommerceDbContext>
    {
        public EcommerceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EcommerceDbContext>();
            string conn = "Server=localhost;Port=5432;User Id=postgres;Database=postgres;Password=admin";
            optionsBuilder.UseNpgsql(conn);

            return new EcommerceDbContext(optionsBuilder.Options);
        }
    }
}
