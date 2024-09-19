namespace Ecommerce.Domain.Entities.Customers
{
    public class Customer
    {
        public decimal Price { get; set; }
        public Customer(decimal price)
        {
            Price = price;
        }

        public virtual decimal CalculateDiscount()
        {
            //PRECISO FAZER UMA VALIDAÇÃO AQUI PARA NAO DAR DESCONTO PARA UMA VENDA INVALIDA. TALVEZ
            return 0;
        }
    }
}
