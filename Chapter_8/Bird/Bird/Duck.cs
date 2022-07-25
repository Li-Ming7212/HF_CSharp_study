using System;
using System.Collections.Generic;
using System.Text;

namespace Bird {
    enum KindOfDuck {
        Mallard,
        Muscovy,
        Loon,
    }
    class Duck : Bird {
        public int Size { get; set; }
        public KindOfDuck Kind { get; set; }

        public override string ToString() {
            return $"A {Size} inch {Kind}";
        }
    }
}
