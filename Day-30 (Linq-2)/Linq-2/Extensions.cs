using System;
using System.Collections.Generic;
using System.Text;

namespace Linq_2 {
  internal static class Extensions {
    // eager execution
    
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Predicate<T> predicate) {
      List<T> result = new List<T>();

      foreach (var item in list) {
        if (predicate(item)) {
          result.Add(item);
        }
      }

      return result;
    }
    

    // defeared execution with execusion method Filter
    public static IEnumerable<T> Filter2<T>(this IEnumerable<T> list, Predicate<T> predicate) {

      foreach (var item in list) {
        if (predicate(item)) {
          // result.Add(item);
          yield return item;  // bc of yield return, the returnd type must be --> IEnumerable 
        }
      }

      
    }
  }
}
