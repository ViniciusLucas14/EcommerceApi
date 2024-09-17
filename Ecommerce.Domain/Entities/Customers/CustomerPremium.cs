namespace Ecommerce.Domain.Entities.Customers
{
    internal class CustomerPremium : Customer
    {
        private readonly decimal _discountPercentage = 10;
         
        public CustomerPremium(decimal price) : base(price)
        {
            
        }
        public override decimal CalculateDiscount()
        {
            return Price * (_discountPercentage / 100);
        }
    }
}
