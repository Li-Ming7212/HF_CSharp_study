using System;
using System.Collections.Generic;
using System.Text;

namespace PigeonAndOstrich
{
    class Ostrich : Bird
    {
        public override Egg[] LayEggs(int numberOfEggs)
        {
            Egg[] eggs = new Egg[numberOfEggs];
            for (int i = 0; i < numberOfEggs; i++)
            {
                eggs[i] = new Egg(Bird.Randomizer.NextDouble() + 12, "斑点的");
            }
            return eggs;
        }
    }
}
