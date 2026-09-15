using System.Collections.Generic;
namespace LINQtoObject {
  internal class Program {
    static string GetName(Course crs) {
      return crs.Name;
    }
    static void Main(string[] args) {
      #region LINQ(Filter+Choose) 
      IEnumerable<Course> courses = SampleData.Courses.Filter(c => c.Hours > 30);   //1
      IEnumerable<string> names = courses.Choose(c => c.Name);   //2

      IEnumerable<string> names_1 = SampleData.Courses.Choose(GetName);    //v.1
      IEnumerable<string> names_2 = SampleData.Courses.Choose(c => c.Name);   //v.2

      // --------------------------------------------------------------------------
      // use var here 
      // pipe line uses result from function to execute another function
      // instructors' solution 
      // here the return is a separated string(name) and int(hours)
      // iterator will work on the Choose, then Choose will work at Filter 
      var query1 = SampleData.Courses.Filter(c => c.Hours > 30).Choose(c => new {c.Name, c.Hours});

      // my solution
      //IEnumerable<string> result = SampleData.Courses.Filter(c => c.Hours > 30).Choose(c => $"Name:{c.Name}\t Hours: {c.Hours}");
      // my solution return string as a one value returned which mean i can't use these data then as a separated string, int  

      
      foreach (var course in query1) {
        Console.WriteLine($"Name: {course.Name}\t Hours: {course.Hours}");
      }

      #endregion

      Console.WriteLine("---------------------------------------------------------------");

      #region Select and Where 
      var query =
        SampleData.Courses.Where(c => c.Hours > 30)
        .Select(c => new { c.Name, c.Hours });
      // Where like Filter
      // Select like Choose

      foreach (var course in query) {
        Console.WriteLine($"Name: {course.Name}\t Hours: {course.Hours}");
      }
      #endregion

    }
  }
}
