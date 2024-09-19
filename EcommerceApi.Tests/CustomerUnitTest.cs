using Ecommerce.Domain.Entities.Customers;

namespace EcommerceApi.Tests
{
    public class CustomerUnitTest
    {
        [Fact]
        public void DiscountMethod_CustomerPremium_ReturnsOneHundred()
        {
            //Arrange
            var customerPremium = new CustomerPremium(1000);

            // Act
            decimal discountTotal = customerPremium.CalculateDiscount();

            //Assert 
            Assert.Equal(100, discountTotal);
        }
        [Fact]
        public void DiscountMethod_CustomerRegular_ReturnsFifty()
        {
            //Arrange
            var customerRegular = new CustomerRegular(1000);

            // Act
            decimal discountTotal = customerRegular.CalculateDiscount();

            //Assert 
            Assert.Equal(50, discountTotal);
        }
        [Fact]
        public void DiscountMethod_CustomerPremium_ReturnsOneHundredAndFifty()
        {
            //Arrange
            var customerVIP = new CustomerVIP(1000);

            // Act
            decimal discountTotal = customerVIP.CalculateDiscount();

            //Assert 
            Assert.Equal(150, discountTotal);
        }
    }
}
