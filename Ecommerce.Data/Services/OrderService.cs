using Ecommerce.Data.Context;
using Ecommerce.Data.Models;

namespace Ecommerce.Domain.Services
{
    public class OrderService
    {
        private readonly EcommerceDbContext _db;
        public OrderService() { }
        public OrderService(EcommerceDbContext db)
        {
            _db = db;
        }

        public bool OrderAlreadyExists(Guid indentify) {
            return _db.Orders.Any(i => i.Guid == indentify);
        }
        public Products? GetProductById(int id)
        {
            return _db.Products.Where(i => i.Id == id).FirstOrDefault();
        }
        public bool AddProduct(Products product)
        {
           _db.Products.Add(product);
           int created = _db.SaveChanges();
           return created > 0;
        }
        public Customers? GetCustomerById(Guid guid)
        {
            return _db.Customers.Where(i => i.Guid == guid).FirstOrDefault();
        }
        public bool AddCustomer(Customers customer)
        {
            _db.Customers.Add(customer);
            int created = _db.SaveChanges();
            return created > 0;
        }

        public bool AddOrder(Orders order)
        {
            _db.Orders.Add(order);
            int created = _db.SaveChanges();
            return created > 0;
        }
        public bool AddOrderItems(List<OrderItems> orderItems)
        {
            _db.OrderItems.AddRange(orderItems);
            int created = _db.SaveChanges();
            return created > 0;
        }
    }
}
