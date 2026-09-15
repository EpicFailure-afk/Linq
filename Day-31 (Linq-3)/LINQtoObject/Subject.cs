using System;
using System.Collections.Generic;
using System.Text;

namespace LINQtoObject {
  internal class Subject {
    public string Name { get; set; }
    public string Description { get; set; }

    public override string ToString() {
      return Name;
    }
  }
}
