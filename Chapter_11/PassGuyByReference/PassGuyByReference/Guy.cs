using System;
using System.Collections.Generic;
using System.Text;

namespace PassGuyByReference {
#nullable enable
    class Guy {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public override string ToString() => $"a {Age}-year-old named {Name}";

        public Guy( string name, int age ) {
            Name = name;
            Age = age;
        }
    }

}
