namespace IntroTolinq_2 {
  delegate int mathOperation(int x, int y);
  internal class Program {
    #region delegate
    //method add
    static int Add(int x, int y) {
      return x + y;
    }

    // method subtraction
    static int subtract(int x, int y) {
      return x - y;
    }
    #endregion

    #region simple function with predicate
    /*
    static bool IsPositive(int number) {
      return number > 0;
    }
    */
    #endregion

    #region filter function
    
    static bool IsPositive(int number) {
      return number > 0;
    }
    

    static void Filter(List<int> numbers, Predicate<int> check) {
      foreach(int number in numbers) {
        if (check(number)) {
          Console.WriteLine(number);
        }
      }
    }
    
    #endregion

    static void Main(string[] args) {
      #region Lists
      /* LIST_1
      List<int> myList = new List<int>();

      myList.Add(10);
      myList.Add(20);
      myList.Add(30);
      myList.Add(40);
      foreach(int number in myList) {
        Console.WriteLine(number);
      }
      */

      /* LIST_2
      List<int> numbers2 = new() {
        1, 2, 3,4 ,2 ,3 , 5, 2, 3, 4, 6, 5, 3, 4, 2, 5, 6
      };

      foreach (int number in numbers2) {
        Console.WriteLine(number);
      }
      */

      // list employee
      /*
      List<Employee> employees = new List<Employee>();

      employees.Add(
          new Employee {
            ID = 1,
            Name = "Ahmed",
            salary = 5000
          }
        );

      employees.Add(
          new Employee {
            ID = 2,
            Name = "Mohammed",
            salary = 500
          }
        );

      foreach (var Name in employees) {
        Console.WriteLine(Name.ID);
      }
      */
      #endregion
      Console.WriteLine("---------------------------------");

      #region dlegates
      // add 
      /*
      mathOperation operation;
      operation = Add;
      int result = operation(4, 5);
      Console.WriteLine(result);
      */

      // subtract
      /*
      operation = subtract;
      int resultSubtract = operation(8, 5);
      Console.WriteLine(resultSubtract);
      */
      #endregion
      Console.WriteLine("---------------------------------");

      #region simple examble on Predicate
      // Predicate<T> is a builtin delegate
      /*
      Predicate<int> check = IsPositive;
      Console.WriteLine(check(20));
      
      */
      #endregion

      Console.WriteLine("---------------------------------");

      #region another predicate examble with List
      /*
      List<int> numbers = new List<int> {
        -5, 10, -2, 20
      };

      foreach(int number in numbers) {
        if (check(number)) {
          Console.WriteLine(number);
        }
      }
      */
      #endregion

      Console.WriteLine("---------------------------------");
      #region filter using ISPositive function
      List<int> numbers = new() {
        -5, 10, -2, 20
      };
      Filter(numbers, IsPositive);
      #endregion

      Console.WriteLine("---------------------------------");

      #region Filter with lambda
      List<int> numbers2 = new() {
        3, 4, -5, 6, -9
      };

      // positive
      Console.WriteLine("positive numbers:");
      Filter(numbers2, x => x > 0);

      //negative
      Console.WriteLine("negative numbers:");
      Filter(numbers2, x => x < 0);

      Console.WriteLine("with extension method:");
      numbers2.Filter(x => x > 0);
      #endregion
    }
  }
}
