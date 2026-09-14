using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Linq_2 {
  internal class Program {

    static IEnumerable<int> Sequence() {
      //List<int> list = new List<int>() { 1, 2, 3 };
      //return list;  // here func is called only one time and return only one time
      //                    that means eager execution 

      yield return 1; // yield makes the execution deferred till iteration 

      // subroutine ---> is the normal function
      // yield make the function ---> coroutine: continue from the last point
      //                                instead of returning what the fun return as one package 
      //                              that mean defeard execution

      yield return 2;

      yield return 3;
    }
    static void Main(string[] args) {
      // part one
      
      IEnumerable<int> list = Sequence(); // list is ref that point at the created obj
      // list in the subroutine concept is the carreing the data that implemented by Sequence()
      // but lit at the coroutine concept only caries the function itself without implementation yet


      foreach (int item in list) {
        Console.WriteLine(item);
      }

      Console.WriteLine("--------------------------------------------");

      // part two, with yield return
      var list2 = new List<int>() { 1, 2, 3, -4, 5, -6, -7, 8, 9 };
      IEnumerable<int> newList = list2.Filter2(x => x % 2 == 0);
      //newList does not contain the filtered data yet.
      //It references an IEnumerable that represents
      //the deferred filtering operation.
      foreach (int item in newList) {
        Console.WriteLine(item);
      }
    }
  }
}
