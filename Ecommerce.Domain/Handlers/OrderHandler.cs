using Ecommerce.Data.Models;
using Ecommerce.Data.Services;
using Ecommerce.Domain.Commands;
using Ecommerce.Domain.Entities.Customers;
using Ecommerce.Domain.Entities.Sti3;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Domain.Handlers
{   
    public class OrderHandler : IRequestHandler<OrderCommand, ContentResult>
    {
        private OrderService _orderService; 
        private Sti3Service _sti3Service;
        public OrderHandler(OrderService orderService, Sti3Service sti3Service)
        {
            _orderService = orderService;
            _sti3Service = sti3Service;
        }
        public async Task<ContentResult> Handle(OrderCommand command, CancellationToken cancellationToken)
        {
            #region OrderValidator
            if (_orderService.OrderAlreadyExists(command.Identify))
            {
                return new ContentResult
                {
                    Content = "Order Identity Already Exists. Please, create another identificador",
                    StatusCode = StatusCodes.Status409Conflict
                };
            }
            #endregion

            #region ProductValidator
            foreach (var product in command.Items)
            {

                bool productNotExists = _orderService.GetProductById(product.ProductId) == null;
                if (productNotExists)
                {
                    Products newProduct = new Products() { Description = product.Description, Id = product.ProductId, Price = product.UnitPrice };
                    bool insertedSucceed = _orderService.AddProduct(newProduct);
                    if (!insertedSucceed)
                    {
                        return new ContentResult
                        {
                            Content = "The new product was not inserted. Try again or later",
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                    }
                }
            }
            #endregion

            #region CustomerValidate
            Customers? customer = _orderService.GetCustomerById(command.Customers.ClientId);
            if (customer == null) {
                Customers newCostumer = new Customers() {
                    Category = command.Customers.Category.ToString(), 
                    CPF = command.Customers.CPF, 
                    Guid = command.Customers.ClientId, 
                    Name = command.Customers.Name,             
                };
                bool insertedSucceed =  _orderService.AddCustomer(newCostumer);
                customer = newCostumer;
                if (!insertedSucceed)
                {
                    return new ContentResult
                    {
                        Content = "The new customer was not inserted. Try again or later",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            #endregion

            #region CreatingOrderAndOrderItems
            decimal orderSubtotal = command.Items.Sum(p => p.UnitPrice * p.Quantity);
            Customer customerType = CustomerType(command.Customers.Category, orderSubtotal);
            decimal Orderdiscount = customerType.CalculateDiscount();
            decimal Ordertotal = orderSubtotal - Orderdiscount;
            Orders newOrder = new Orders() { 
                CustomersId = customer.Id, 
                Date = command.OrderDate,
                Guid = command.Identify, 
                Discount = Orderdiscount, 
                Subtotal = orderSubtotal, 
                Total = Ordertotal
            };
            bool orderCreated = _orderService.AddOrder(newOrder);

            List<OrderItems> newOrderItems = command.Items.Select(o => new OrderItems() {
                ProductsId = o.ProductId, 
                OrdersId = newOrder.Id,
                Total = o.UnitPrice * o.Quantity, 
                Subtotal = o.UnitPrice * o.Quantity,
                Discount = 0,
                Quantity= o.Quantity
            }).ToList();

            _orderService.AddOrderItems(newOrderItems);
            #endregion

            if (!orderCreated)
                return new ContentResult
                {
                    Content = $"Order {newOrder.Guid} was not created",
                    StatusCode = StatusCodes.Status500InternalServerError
                };

            await sendOrderToSti3(newOrder, command.Items);

            return new ContentResult
            {
                Content = $"Order {newOrder.Guid} created successfully",
                StatusCode = StatusCodes.Status201Created
            };           
        }
        private Customer CustomerType(DiscountTypeEnum type, decimal totalOrder)
        {
            if(type == DiscountTypeEnum.REGULAR && totalOrder > 500)
            {
                return new CustomerRegular(totalOrder);
            }
            else if (type == DiscountTypeEnum.PREMIUM && totalOrder > 300) 
            {
                return new CustomerPremium(totalOrder);
            }
            else if (type == DiscountTypeEnum.VIP)
            {
                return new CustomerVIP(totalOrder);
            }
            else
            {
                return new Customer(totalOrder);
            }
        }
        private async Task sendOrderToSti3(Orders ordered, List<OrderCommand.Item> products)
        {
            var order = new OrderSti3()
            {
                Descontos = ordered.Discount,
                Identificador = ordered.Guid.ToString(),
                Subtotal = ordered.Subtotal,
                ValorTotal = ordered.Total,
                Itens = products.Select(o => new ItemSti3(o.Quantity,o.UnitPrice)).ToList()
            };
            string json = JsonConvert.SerializeObject(order, Formatting.Indented);
            await _sti3Service.SendOrderToSti3Async(json);
            var a = json;
        }
    }
}
