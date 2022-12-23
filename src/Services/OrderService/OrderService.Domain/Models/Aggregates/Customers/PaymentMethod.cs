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
    public string CardHolderName { get; private set; }
    public string CardNumber { get; private set; }
    public DateTime ValidTru { get; private set; }

    // hangi tip kart ile işlem yapıldı
    public int CardTypeId { get; private set; }
    public CardType CardType { get; set; }


    public PaymentMethod(string cardNumber,DateTime validTru,string cardHolderName, int cardTypeId)
    {
      Id = Guid.NewGuid().ToString();
      CardNumber = cardNumber;
      CardHolderName = cardHolderName;
      CardTypeId = cardTypeId;
      ValidTru = validTru;
    }


  }
}
