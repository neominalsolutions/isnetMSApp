using MediatR;
using OrderService.Domain.Events;
using OrderService.Domain.Models.Aggregates.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DomainEventHandlers
{
  // IntegrationEventHandler olsaydı Masstransit IConsume interface kullanılacaktı. fakat bu bir Domain event Handler olduğu için MediatR INotificationHandler kullanıcağız
  public class OrderSubmittedDomainEventHandler : INotificationHandler<OrderSubmitted>
  {
    private readonly ICustomerRepository customerRepository;
    public OrderSubmittedDomainEventHandler(ICustomerRepository customerRepository)
    {
      this.customerRepository = customerRepository;
    }
    public async Task Handle(OrderSubmitted notification, CancellationToken cancellationToken)
    {

      var existingCustomer = (await this.customerRepository.GetAsync(x => x.CustomerName == notification.CustomerName));

      if(existingCustomer != null)
      {
        // bu müşterinin paymentMethod varsa direk onu kullanıcaz yoksa yeni bir payment method ekleyeceğiz.
        var paymentMethod = existingCustomer.VerifyPaymentMethod(notification.CardTypeId, notification.CardHolderName, notification.CardNumber, notification.ValidThru);

        var customerId = existingCustomer.Id;
        var paymentMethodId = paymentMethod.Id;

        notification.SubmitOrder.SetCustomerId(customerId);
        notification.SubmitOrder.SetPaymentMethodId(paymentMethodId);

      } 
      else
      {
        // müşteri yoksa yeni müşteri oluştur ona kart bilgileri gir
        var customer = new Customer(customerName: notification.CustomerName);
        var paymentMethod = new PaymentMethod(notification.CardNumber, notification.ValidThru, notification.CardHolderName, notification.CardTypeId);

        customer.AddPaymentMehod(paymentMethod);

        await this.customerRepository.AddAsync(customer);

        // submit order nesnesini güncelle
        notification.SubmitOrder.SetCustomerId(customer.Id);
        notification.SubmitOrder.SetPaymentMethodId(paymentMethod.Id);

      }

      // MS düzeyinde iş akışı
      // Customer var mı yok mu  yoksa customer oluştur
      // Customer Payment Method var mı yok mu yoksa payment method oluştur
      // order nesnesinde customerId alanını set et
      // order nesnesinde paymentMethodId alanını set et

      // MS Scope dışındaki MS arası iş akışımız

      // git service Bus bağlan ve Product Stock değişimi için integration event publish et.


    }
  }
}
