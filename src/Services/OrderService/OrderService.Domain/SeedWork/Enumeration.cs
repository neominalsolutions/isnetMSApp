using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.SeedWork
{
  // enum olarak yazılacak sabitler varsa ddd bu sabitler için enum kullanımından ziyade enumeration sınıfından sınıfı türetmemizi ICompare interface kullanarak sınıfın içindeki sabitlerin birbileri kıyaslanacak şekilde yazılmasını öneriyor.

  // Visa = 1,MasterCard = 2

  // PaymentCard:Enumeration

  //[{id:1,name:'visa'}]
  public abstract class Enumeration : IComparable
  {
    public string Name { get; set; } // Enum Name String Text alanı, Programdaki karşılığı

    public int Id { get; set; } // id 1,2,3  gibi db deki karşılığı


    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
           typeof(T).GetFields(BindingFlags.Public |
                               BindingFlags.Static |
                               BindingFlags.DeclaredOnly)
                    .Select(f => f.GetValue(null))
                    .Cast<T>();


    // FindOne x=> x.name=='visa'
    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
      var matchingItem = GetAll<T>().FirstOrDefault(predicate);

      if (matchingItem == null)
        throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

      return matchingItem;
    }


    public static T FromValue<T>(int value) where T : Enumeration
    {
      var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
      return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
      var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
      return matchingItem;
    }

    // şuan gönderilen obje id si nesnenin idsine ile aynı mı değil mi
    public int CompareTo(object? obj)
    {
      return Id.CompareTo(((Enumeration)obj).Id);
    }

    // nesne enumration dan mı türüyor ve tip ve value eşitliği varmı
    public override bool Equals(object obj)
    {
      if (obj is not Enumeration otherValue)
      {
        return false;
      }

      var typeMatches = GetType().Equals(obj.GetType());
      var valueMatches = Id.Equals(otherValue.Id);

      return typeMatches && valueMatches;
    }

    // class dnası her class'ın c# tarafında bir hashcode vardır.
    public override int GetHashCode() => Id.GetHashCode();

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
      var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
      return absoluteDifference;
    }
  }
}
