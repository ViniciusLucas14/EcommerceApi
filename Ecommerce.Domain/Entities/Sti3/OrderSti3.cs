using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Sti3
{
    public class OrderSti3
    {
        public string Identificador { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descontos { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemSti3> Itens { get; set; }
    }
    public class ItemSti3
    {
      
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public ItemSti3(decimal quantidade, decimal precoUnitario)
        {
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

    }
}
