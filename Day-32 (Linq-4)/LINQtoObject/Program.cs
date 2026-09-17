namespace LINQtoObject {
  internal class Program {
    static void Main(string[] args) {
      #region PipeLine
      // Where then Select
      var query =
        SampleData.Courses.Where(c => c.Hours > 30)   // the return reom select is IEnumerable of Course
        .Select(c => new { c.Name, c.Hours });  // Select will work on IEnumerable of course 
            // that means we can access Department from the c object

      // Select then Where 
      var query_1 =
        SampleData.Courses.Select(c => new { c.Name, c.Hours }) // the return from select is IEnumerable of Anonymous  
        .Where(c => c.Hours > 30);  // Where will work on IEnumerable of Anonymous
                                    // that means c is objecy of Anonymous wich only contain Name and Hours
                                    // so we can't access Department fron that object

      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region query operator & query expression
      // where and select are operators
      // this query is query oprator ---> is the best to use
      var query_2 =
        SampleData.Courses.Where(c => c.Hours > 30)   
        .Select(c => new { c.Name, c.Hours });

      // query expression
      // must start with from and
      // must end with one of two (select or group by)
      var query_3 =
        from crs_1 in SampleData.Courses
        where crs_1.Hours > 30
        select new { crs_1.Name, crs_1.Hours };
      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region Take and Skip
      // select top 3 
      // in case of query operator will occur 2 steps
      // the best performance
      // Take()
      var query_4 =
        SampleData.Courses.Where(c => c.Hours > 30)
        .Select(c => new { c.Name, c.Hours }).Take(2);

      // Skip()
      var query_5 =
        SampleData.Courses.Where(c => c.Hours > 30)
        .Select(c => new { c.Name, c.Hours }).Skip(2);

      // TakeWhile() --> its parameter is condition 
      // select the aresult till get false
      // يعني اول ما يلاقي حاجه مش محققه الشرط هيطلع حتي لو في حاجه بعدها محققه الشرط 
      var query_6 =
        //SampleData.Courses.TakeWhile(c => c.Hours > 30);
        SampleData.Courses.SkipWhile(c => c.Hours > 30);
      // operator expression
      // to performance Take() on a query expression must serround the query with (query)
      // then use .Take
      // that cause a leak in performance bc of there is an additional step will occur
      // here there are 3 stpes
      // Take()
      var query_7 =
        (from crs_2 in SampleData.Courses
         where crs_2.Hours > 30
         select new { crs_2.Name, crs_2.Hours }).Take(2);
      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region Aggr Functions
      // Count()
      // Count here is eager execution
      int coursesCount = SampleData.Courses.Count();
      
      // return the same result:  
      int coursesCount_1 = SampleData.Courses.Count(c => c.Hours > 30);  
      int coursesCount_1_1 = SampleData.Courses.Where(c => c.Hours > 30).Count();
      // count here is eager but it works on an operation that deferred executed
      // count will foreach on the IEnumerable that return from Where

      // Sum()
      // query operator
      int TotalHours =
        SampleData.Courses
        .Where(c => c.Department.Name == "SD")
        .Sum(c => c.Hours);

      // query expression
      // NOTE: performance leak 
      int TotalHours_1 =
        (from crs_3 in SampleData.Courses
         where crs_3.Department.Name == "SD"
         select crs_3.Hours).Sum();

      // Max()
      //Course maxCourse = SampleData.Courses.Max();  // will throw an exception, Max depends on what ?
      // So we must implement IComparable

      int maxHours = SampleData.Courses.Max(c => c.Hours);

      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region Join
      // join with query expression
      // we don't have ID in SampleData but this examble in case we have ID to perform the join
      /*
      var query_8 =
        from dept in SampleData.Departments
        from crs_4 in SampleData.Courses
        where dept.ID == crs_4.DepartmentID
        //select new { dept.Name, crs.Name } // compiler refuses here 
        // because the new anonymous obj carries two props of Name and thats error
        select new { deptName = dept.Name, crsName = crs_4.Name };

      // another way to join 
      var query_9 =
        from dept in SampleData.Departments
        join crs_5 in SampleData.Courses
        on dept.ID equals crs_5.DepartmentID
        select new { deptName = dept.Name, crsName = crs_5.Name };
      */
      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region First & Last
      Course crs = SampleData.Courses.Where(c => c.Hours == 60).FirstOrDefault();  // or .First()
      // FirstOrDefault() will return an object not IEnumerable like Take() 
      // in case of First() if there is null at return will throw an exception 
      Course crs_7 = SampleData.Courses.Where(c => c.Hours == 60).LastOrDefault();  // or .Last()
        // LastOrDefault() works like FirstOrDefault() but it returns the last one 
      #endregion

      Console.WriteLine("---------------------------------------------------------------------");
      
      #region SubQuery
      // how to use subquery
      // return each course name with its subjects and its hours
      var query_10 =
        from sub in SampleData.Subjects
        select new {
          SubName = sub.Name,
          Courses =
            from crs in SampleData.Courses
            where crs.Subject.Name == sub.Name
            select crs
        };

      foreach (var sub in query_10) {
        Console.WriteLine($"SubName: {sub.SubName}\t TotalHours: {sub.Courses.Sum(c => c.Hours)}");
        Console.WriteLine();

        foreach (var crs_8 in sub.Courses) {
          Console.WriteLine($"Name: {crs_8.Name}\t Hours: {crs_8.Hours}");
        }

        Console.WriteLine("---------------------------------------"); 
      }
      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region OrderBy
      // query operator
      var query_11 =
        SampleData.Courses.Where(c => c.Hours > 30)
        .Select(c => new { c.Name, c.Hours })
        .OrderByDescending(c => c.Hours)
        .ThenBy(c => c.Name);  // can only pick Name or Hours
          // ThenBy like OrderBy but can apply on the last Order
          // on the other hand OrderBy orders from the beggining 
          // if i wanna order by DepartmentID which is not returnd in the Anonymous object that came from the select 
          // i must write the OrderBy after the Where and before the Select 

      var query_12 =
        from crs_12 in SampleData.Courses
        where crs_12.Hours > 30
        orderby crs_12.Hours descending, crs_12.Name ascending
        select new { crs_12.Name, crs_12.Hours };

      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region from deferred exec to eager exec
      var query_13 =
        SampleData.Courses.Where(c => c.Hours > 30)
        .Select(c => new { c.Name, c.Hours })
        .OrderByDescending(c => c.Hours)
        .ThenBy(c => c.Name)
        .ToList(); // means execute the query now and put the result in List
        // this query means {list of Anonymous}
        // means the data already exists at query_13

      foreach (var item in query_12) {
        Console.WriteLine($"Name: {item.Name}\t Hours: {item.Hours}");
      }

      #endregion

      Console.WriteLine("---------------------------------------------------------------------");

      #region group by
      // IGrouping --> Interface anything implement that Interface will have a key and Implements IEnumerable
      var query_14 =
        from crs_5 in SampleData.Courses
        group crs_5 by crs_5.Subject;
      
      foreach (var grp in query_14) {
        Console.WriteLine($"Subject: {grp.Key.Name}\t TotalHours: {grp.Sum(c => c.Hours)}");
        Console.WriteLine();
        foreach (var crs_5 in grp) {
          Console.WriteLine($"Name: {crs_5.Name}\t Hours: {crs_5.Hours}");
        }
        Console.WriteLine("------------------------------------");
      }

      // grop by + having
      var query_15 =
        from crs_6 in SampleData.Courses
        group crs_6 by crs_6.Subject into grp
        let totalHours = grp.Sum(c => c.Hours)
        // let makes you declare variable inside query
        // let only works with query expression 
        // can't work with query operator
        where totalHours > 60
        select new { subjectName = grp.Key.Name, Courses = grp, TotalHours = totalHours };

      foreach (var grp in query_15) {
        Console.WriteLine($"Subject: {grp.subjectName}\t TotalHours: {grp.TotalHours}");
        Console.WriteLine();
        foreach (var crs_6 in grp.Courses) {
          Console.WriteLine($"Name: {crs_6.Name}\t Hours: {crs_6.Hours}");
        }
        Console.WriteLine("------------------------------------");
      }
      #endregion
    }
  }
}
