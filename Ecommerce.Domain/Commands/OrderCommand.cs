using Ecommerce.Data.Models;
using Ecommerce.Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace Ecommerce.Domain.Commands
{
     public class OrderCommand :  Notifiable<Notification>, IRequest<ContentResult>
    {

        [JsonPropertyName("dataVenda")]
        public DateTimeOffset OrderDate { get; set; }
        [JsonPropertyName("identificador")]
        public Guid Identify { get; set; }

        public OrderCommand(DateTimeOffset orderDate, Guid identify)
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNull(identify, nameof(identify), "identify is empty"));
            OrderDate = orderDate;
            Identify = identify;
        }
        public partial class Customer
        {
            [JsonPropertyName("clienteId")]
            public Guid ClientId { get; set; }
            [JsonPropertyName("nome")]
            public string Name { get; set; } = "";
            [JsonPropertyName("cpf")]
            public string CPF { get; set; } = "";

            [JsonPropertyName("categoria")]
            public DiscountTypeEnum Category{ get; set; }
            public Customer(Guid clientId, string name, string cpf, DiscountTypeEnum category)
            {
                ClientId = clientId;
                Name = name;
                CPF = cpf.Replace(".", "").Replace("-", ""); ;
                Category = category;
            }
        }
        public partial class Item
        {
            [JsonPropertyName("produtoId")]
            public int ProductId { get; set; }
            [JsonPropertyName("descricao")]
            public string Description { get; set; } = "";
            [JsonPropertyName("quantidade")]
            public decimal Quantity { get; set; }
            [JsonPropertyName("precoUnitario")]
            public decimal UnitPrice { get; set; }
            public Item(int productId, string description, decimal quantity, decimal unitPrice)
            {
                ProductId = productId;
                Description = description;
                Quantity = quantity;
                UnitPrice = unitPrice;
            }
        }
        [JsonPropertyName("Cliente")]
        public Customer Customers { get; set; }

        [JsonPropertyName("Itens")]
        public List<Item> Items { get; set; }
       
    }
}
