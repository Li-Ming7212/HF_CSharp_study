using System;
using System.Collections.Generic;

namespace Ducks {
    internal class Program {
        static void Main(string[] args) {
            List<Duck> ducks = new List<Duck>() {
                new Duck() { Kind = KindOfDuck.绿头鸭, Size = 17 },
                new Duck() { Kind = KindOfDuck.番鸭, Size = 18 },
                new Duck() { Kind = KindOfDuck.洛翁, Size = 14 },
                new Duck() { Kind = KindOfDuck.番鸭, Size = 11 },
                new Duck() { Kind = KindOfDuck.绿头鸭, Size = 14 },
                new Duck() { Kind = KindOfDuck.洛翁, Size = 13 },
                };

            //ducks.Sort();

            //IComparer<Duck> sizeComparer = new DuckComparerBySize();
            //IComparer<Duck> kindComparer = new DuckComparerByKind();
            //ducks.Sort(sizeComparer);
            //ducks.Sort(kindComparer);

            //PrintDucks(ducks);

            DuckComparer comparer = new DuckComparer();
            Console.WriteLine("\n根据尺寸排列\n");
            comparer.SortBy = SortCriteria.KindThenSize;
            ducks.Sort(comparer);
            PrintDucks(ducks);
            Console.WriteLine("\n根据种类排列\n");
            comparer.SortBy = SortCriteria.SizeThenKind;
            ducks.Sort(comparer);
            PrintDucks(ducks);

        }

        public static void PrintDucks(List<Duck> ducks) {
            foreach (Duck duck in ducks) {
                //Console.WriteLine($"{duck.Size} 英寸 {duck.Kind}");
                Console.WriteLine(duck);
            }
        }
    }
}