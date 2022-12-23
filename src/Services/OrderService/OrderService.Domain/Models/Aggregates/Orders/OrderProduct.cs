using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models.Aggregates.Orders
{
  // sipariş edilmiş ürün
  public class OrderProduct
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal ListPrice { get; set; }

   
  }
}
