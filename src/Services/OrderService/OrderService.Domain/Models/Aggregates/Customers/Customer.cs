using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models.Aggregates.Customers
{
  // customer aggregateroot olduğu için cardType girişini, PaymentMethod girişini kendi nesnesi üzerinden yönetmelidir.
  public class Customer:AggregateRoot
  {
    public string CustomerName { get; private set; }

    // müşterinin birden fazla ödeme yöntemi olabilir.
    private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();

    public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods;
    public Customer(string customerName)
    {
      CustomerName = customerName;
      Id = Guid.NewGuid().ToString();
    }

    // Müşterinin gönderdiği bilgilere göre ödeme şeklini kontrol ettik. sistemde böyle bir ödeme şekli var mı yok mu kontrolü yapılıd.
    // müşteri ödeme yöntemi girişi kontrolü sağlandı.
    public PaymentMethod VerifyPaymentMethod(int cardTypeId,string cardHolderName,string cardNumber, DateTime validTru)
    {
      var existingPayment = _paymentMethods.FirstOrDefault(x => x.CardTypeId == cardTypeId && x.CardHolderName == cardHolderName && x.CardNumber == cardNumber);

      if(existingPayment == null)
      {
        var payment = new PaymentMethod(cardNumber, validTru, cardHolderName, cardTypeId);

        _paymentMethods.Add(payment);
        // domain event fırlatarak card bilgisinin db kaydedilmesi için bir eylem gerçekleştirice

        return payment;
      }
      else
      {
        // domain event fırlatarak card bilgisinin db kaydedilmesi için bir eylem gerçekleştirice
        return existingPayment;
      }
    }

  }
}
