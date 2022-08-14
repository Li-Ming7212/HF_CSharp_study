using System;
using System.Collections.Generic;
using System.Text;

namespace ClownWithLambdas {
    class TallGuy : IClown {
        public string Name;
        public int Height;

        public string FunnyThingIHave => "一双大红鞋鞋";

        public void Honk() => Console.WriteLine("Honk honk!");


        public void TalkAboutYourself() {
            Console.WriteLine($" 我叫 {Name} ， 我身高 {Height} 英寸.");
        }
    }

}
