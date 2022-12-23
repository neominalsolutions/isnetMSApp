using OrderService.Domain.Events;
using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models.Aggregates.Orders
{
  public class Order:AggregateRoot
  {
    public int OrderStatusId { get; set; } // 100,200,300,400,500
   
    // Sipariş veren müşteri sistemde kayıtlı bir müşteri olmayabilir. guest müşteri olabilir. Bu durumda customerId değerini sahip olmayabilirim fakat bu siparişin kim tarafından verildiğini bilmek zorundayım bundan dolayı customer kaydı yaparız. CustomerName ve kart üzerindeki alanlara göre yeni bir müşteri kaydı yaparız.
    public string CustomerId { get; set; } // Bunu Customer Repodan alıcaz

    // Bunuda bilemiyoruz nedeni ise sistemde var olan bir müşteri ödeme yöntemi olabilir yada yeni sisteme eklenecek bir ödeme yöntemi olabilir. 
    public string PaymentMethodId { get; set; }

    public DateTime OrderDate { get; private set; }

    public ShipAddress ShipAddress { get; set; } // Kargo adresi

    // fieldan value okucam
    private List<OrderItem> _orderItems = new List<OrderItem>();

    // property readonly yaptık dışarıdan listeye ekleme çıkarma yapılamasın çünkü orderItem eklerken belli logicleri devreye sokabilirim. Bu sebeple bunu daha kontrollü yapmak için readOnly yaptık

    // Eğer List Yaparsak OrderItem Contructora bakalım Örnek orada
    //public List<OrderItem> OrderItems => _orderItems;
    public IReadOnlyList<OrderItem> OrderItems => _orderItems;


    public Order(string customerName,string city,string country,string street,string cardNumber,DateTime validThru,string cardHolderName, int cardTypeId)
    {
      Id = Guid.NewGuid().ToString();
      OrderStatusId = OrderStatus.Submitted.Id; // veri tabanına order ilk state submitted
      OrderDate = DateTime.UtcNow; // Greenwich göz önünde bulunduralım
      ShipAddress = new ShipAddress(city, country, street);

      AddDomainEvent(new OrderSubmitted(customerName, cardNumber, validThru, cardHolderName, cardTypeId));

      // yani tam bu noktada müşteri ve müşteriye ait ödeme yöntemi yok ise bunların tek bir transaction altında tanımlanmasını sağlayacak bir event ayağa kaldıralım.

    }

    public void AddItem(string productName,string productId,int quantity,decimal listPrice)
    {
      // event fırlatıp bu ürünün stoğunu kontrol edebilirim. eğer stokta ürün yoksa exception fırlatabilirim.
      // aynı üründen 1 kerede 20 adet alınmasını engelleyebilirim. Maksimum Stock Threshold bir kontrol yaparım.
      // her bir ürün siparişinde bir log atabilirim.

      if(quantity >= 20)
      {
        throw new MaximumStockOverFlow(errorMessage:"Maksimum stok sayısı ürün başına 20 geçemez");
      }

      _orderItems.Add(new OrderItem(productId: productId, orderId: this.Id, productName: productName, listPrice: listPrice));


    }

    // nesne oluşturuken bilmilyorum. Fakat nesne oluşturulduğu aşamada öğrenip dbden çekip bu alanları set edicez.
    public void SetCustomerId(string customerId)
    {
      CustomerId = customerId;
    }

    public void SetPaymentMethodId(string paymentMethodId)
    {
      PaymentMethodId = paymentMethodId;
    }


    public Order()
    {

      //if(OrderStatusId == OrderStatus.Shipped.Id)
      //{
          //var orderStatus = OrderStatus.FromValue<OrderStatus>(100).Name; // submitted
      //}


    }
  }
}
