using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.SeedWork
{

  // dbde tutulmayan sadece program tarafında yaşıyan içinde id kimlik bilgisi barındırmayan. mutable yapılara valueobject denir. Koordinat sistemi x,y bilgileri yeterlidir. x,y ile değer oluşturuğunda bu değer value object olarak kullanılabilir. OrderAddress City,Country,State
  public abstract class ValueObject
  {

    // referans eşitliği var mı aynı nesnemi
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
      if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
      {
        return false;
      }
      return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
      return !EqualOperator(left, right);
    }

    // nesne içerisindeki propertylerin değerlerini dışarı aktarmamız sağlar.
    // nesneye ait property değerlerini kıyaslanmasında kullanılır
    protected abstract IEnumerable<object> GetEqualityComponents();


    // tip eşitliği var mı
    public override bool Equals(object obj)
    {
      if (obj == null || obj.GetType() != GetType())
      {
        return false;
      }

      var other = (ValueObject)obj;

      return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    // nesne hashcode üretimi için kullanılan method
    public override int GetHashCode()
    {
      return GetEqualityComponents()
       .Select(x => x != null ? x.GetHashCode() : 0)
       .Aggregate((x, y) => x ^ y);
    }

    // Nesnenin birebir yanı referasnda klonlamak için var
    // MemberwiseClone bu işe yarar
    public ValueObject GetCopy()
    {
      return MemberwiseClone() as ValueObject;
    }
  }
}
