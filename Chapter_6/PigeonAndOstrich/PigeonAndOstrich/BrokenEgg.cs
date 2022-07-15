using System;
using System.Collections.Generic;
using System.Text;

namespace PigeonAndOstrich
{
    class BrokenEgg : Egg
    {
        public BrokenEgg(string color) : base(0, $"破碎的 {color}")
        {
            Console.WriteLine("一只鸟下了一个破碎的蛋");
        }
    }
}
