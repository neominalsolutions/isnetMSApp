using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models.Aggregates.Customers
{
  // müşterinin ödeme methodu
  // Entity ama Aggregate Root değil bunun AggregateRootu Customer
  public class PaymentMethod
  {
    public string Id { get; set; }

    // Kart üzerindeki isim
    public string CardHolderName { get; private set; }

    // Kart Numarası
    public string CardNumber { get; private set; }

    // Kart Geçerlilik Tarihi
    public DateTime ValidThru { get; private set; }

    // hangi tip kart ile işlem yapıldı (Amex,MasterCard,Visa)
    public int CardTypeId { get; private set; } // 1,2,3
    public CardType CardType { get; set; }

    public PaymentMethod()
    {

    }

    public PaymentMethod(string cardNumber,DateTime validTru,string cardHolderName, int cardTypeId)
    {
      Id = Guid.NewGuid().ToString();
      CardNumber = cardNumber;
      CardHolderName = cardHolderName;
      CardTypeId = cardTypeId;
      ValidThru = validTru;
    }


  }
}
