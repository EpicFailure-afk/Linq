using System;
using System.Collections.Generic;
using System.Text;

namespace LINQtoObject {
  internal static class Extensions {
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> Source, Predicate<T> predicate) {
      foreach (var item in Source) {
        if (predicate(item)) {
          yield return item;
        }
      }
    }

    // --------------------------------------------------------------------------------------

    // Choose fun takes list of courses and return the names (strings) of them 
    // or return the IDs (int) of the courses which mean that fun will deal with 2 dt
    // one as an input another as an output
    public static IEnumerable<TResult> Choose<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> chooser) {
      foreach (var item in source) {
          yield return chooser(item);
        }
      }

    // MyCount function works like Count() that works on the IEnumerable object that came from Where (the filteration)
    // eager exection works on Where that works deferred execution
    public static int MyCount<TSource>(this IEnumerable<TSource> source) {
      int counter = 0;

      foreach (var item in source) {
          counter++;
      }
      return counter;
    }
  }
  }

