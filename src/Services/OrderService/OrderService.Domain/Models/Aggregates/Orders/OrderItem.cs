using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models.Aggregates.Orders
{
  public class OrderItem
  {
    // Composite Key yapmak için OrderId,ProductId alanı kullanırız
    public string OrderId { get; set; }

    // ProductName alanı denormalize olarak tuttuk.
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ListPrice { get; set; }


    // Order ihtiyaç duyduğu ProductNesnesi olarak ilişkilendirip. Ayrıca bir tablo açıyoruz.
    // public string OrderProductId { get; set; }
    //public OrderProduct OrderProduct { get; set; }
    public OrderItem()
    {
      // Order o = new Order();
      //o.OrderItems.Add(new OrderItem());
      //o.OrderItems.Remove(new OrderItem());
      // bad practice kontrolsüz olarak orderItem nesnesine müdehale etme durumu
      // o.AddItem("kazak", "1", 10, 125);

    }

    public OrderItem(string orderId, string productId, string productName, decimal listPrice)
    {
      OrderId = orderId;
      ProductId = productId;
      ProductName = productName;
      ListPrice = listPrice;
    }
  }
}
