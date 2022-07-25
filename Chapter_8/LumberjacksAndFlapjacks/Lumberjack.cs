using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberjacksAndFlapjacks {

    enum Flapjack {
        Crispy, Soggy, Browned, Banana
    }
    internal class Lumberjack {
        public string Name { get; private set; }

        private Stack<Flapjack> flapjackStack = new Stack<Flapjack>();

        public Lumberjack(string name) {
            this.Name = name;
        }

        public void TakeFlapjack(Flapjack flapjack) {
            flapjackStack.Push(flapjack);
        }

        public void EatFlapjacks() {
            Console.WriteLine($"{Name}正在吃烙饼");
            while (flapjackStack.Count > 0) {
                Console.WriteLine($"{Name}吃了一个{flapjackStack.Pop().ToString().ToLower()} 烙饼");
            }
        }
    }
}
