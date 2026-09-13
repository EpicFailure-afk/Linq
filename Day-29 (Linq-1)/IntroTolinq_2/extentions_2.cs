using System;
using System.Collections.Generic;
using System.Text;

namespace IntroTolinq_2 {
  internal static class extentions_2 {
    // only edit on the old filter function is ((((((( this ))))))
    public static void Filter(this List<int> numbers, Predicate<int> check) {
      foreach(int number in numbers) {
        if (check(number)) {
          Console.WriteLine(number);
        }
      }
    }
  }
}
