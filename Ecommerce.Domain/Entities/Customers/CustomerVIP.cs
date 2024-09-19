namespace Ecommerce.Domain.Entities.Customers
{
    public class CustomerVIP : Customer
    {
        private readonly decimal _discountPercentage = 15;

        public CustomerVIP(decimal price) : base(price)
        {

        }
        public override decimal CalculateDiscount()
        {
            return Price * (_discountPercentage / 100);
        }
    }
}
