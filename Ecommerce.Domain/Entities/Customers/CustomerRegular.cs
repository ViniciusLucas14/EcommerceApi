namespace Ecommerce.Domain.Entities.Customers
{
    internal class CustomerRegular : Customer
    {
        private readonly decimal _discountPercentage = 5;

        public CustomerRegular(decimal price) : base(price)
        {

        }
        public override decimal CalculateDiscount()
        {
            return Price * (_discountPercentage / 100);
        }
    }
}
