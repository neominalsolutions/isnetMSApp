using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Events
{
  // event içerisinde veri taşıyan birbirinden bağımsız iki farklı sınıfın birbileri ile haberleşmesini sağlayan yapılar
  // dili geçmiş zaman kipinde kullanılırlar
  // Domain Eventler In Memory çalışır. Aynı microservice düzeyine çalışır
  // Persistance çalışan bir eventten söz ediyorsak bu durumda bu event Integration Eventtir ve Message Broker üzerinden çalışır. 


  // string customerName,string city,string country,string street,string cardNumber,DateTime validTru,string cardHolderName, int cardTypeId
  public class OrderSubmitted:INotification
  {
    public OrderSubmitted(string customerName, string cardNumber, DateTime validThru, string cardHolderName, int cardTypeId)
    {
      CustomerName = customerName;
      CardNumber = cardNumber;
      ValidThru = validThru;
      CardHolderName = cardHolderName;
      CardTypeId = cardTypeId;
    }

    public string CustomerName { get; private set; }
    public string CardNumber { get; private set; }

    public DateTime ValidThru { get; private set; }

    public string CardHolderName { get; private set; }

    public int CardTypeId { get; private set; } // Visa,Mastercard



  }
}
