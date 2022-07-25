using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ducks {
    enum KindOfDuck {
        绿头鸭,
        番鸭,
        洛翁
    }
    internal class Duck : IComparable<Duck> {
        public int Size { get; set; }
        public KindOfDuck Kind { get; set; }

        public int CompareTo(Duck duckToCompare) {
            if (this.Size > duckToCompare.Size) {
                return 1;
            }
            else if (this.Size < duckToCompare.Size) {
                return -1;
            }
            else {
                return 0;
            }
        }

        public override string ToString() {
            return $"{Size} 英寸 {Kind}";
        }
    }
}
