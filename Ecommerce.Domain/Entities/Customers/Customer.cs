namespace Ecommerce.Domain.Entities.Customers
{
    internal class Customer
    {
        public decimal Price { get; set; }
        public Customer(decimal price)
        {
            Price = price;
        }

        public virtual decimal CalculateDiscount()
        {
            return 0;
        }
    }
}
