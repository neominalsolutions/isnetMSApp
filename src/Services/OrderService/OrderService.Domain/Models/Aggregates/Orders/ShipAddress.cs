using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models.Aggregates.Orders
{
  // value Object
  // OrderStreet, OrderCity, OrderCountry
  // eğer bir nesne entity özelliği göstermiyorsa ama program tarafında birden fazla property ile bir anlam ifade ediyorsa - (adress) ancak city,country ve street değerleri yazıldığında bir address olarak kullanılabiliyor-. DDD bu tarz nesneleri value Object yapmanızı tavsiye eder. ((GeoLocation: ValueObject), Latitude,Longitude)
  // PersonName ValueObject: FirstName,MiddleName,LastName . Program tarafında bir anlam ifade eder.
  // Person Class => Id,PersonName,IdentityNumber,DateOfBirth // hem program hemde database nesnesi olarak kullanılır.
  // Entityler ValueObjectleri içerisinde property olarak barındırır. Tersi söz konusu bile olmaz. Bir valueObject içinde entity olamaz.
  
  // Anemic Domain Model Person Class
  // Person, (Id,FirstName,MiddleName,LastName,Identity,DateOfBirth)
  // Money:ValueObject (Currency,Amount) 10 TL, 10 $, 10 $
  public class ShipAddress:ValueObject
  {
    public ShipAddress(string city, string country, string street)
    {
      City = city;
      Country = country;
      Street = street;
    }

    public string City { get; private set; }
    public string Country { get; private set; }
    public string Street { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return City;
      yield return Country;
      yield return Street;

      
    }

    public override bool Equals(object obj)
    {
      //var money1 = new Address("İst","Tr","barbaros"); // ref1
      //var money2 = new Address("İst", "Tr", "barbaros"); // ref2
      //// value olarak aynı referans olarak farklı
      //var money3 = money1.GetCopy() as Address; // ref1 aynı hemde value 1
      //money3.Country = "DEU"; // ref olarak aynı value olarak farklı

      return base.Equals(obj);
    }
  }
}
