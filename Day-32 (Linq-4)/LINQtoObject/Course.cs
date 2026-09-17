using System;
using System.Collections.Generic;
using System.Text;

namespace LINQtoObject {
  internal class Course {
    public string Name { get; set; }
    public int Hours { get; set; }
    public Subject Subject { get; set; }
    public Department Department { get; set; }
  }
}
