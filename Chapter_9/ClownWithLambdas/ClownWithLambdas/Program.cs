using System;

namespace ClownWithLambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            TallGuy tallGuy = new TallGuy() { Height = 76, Name = "Jimmy" };
            tallGuy.TalkAboutYourself();
            Console.WriteLine($"这位长得高的伙计有 {tallGuy.FunnyThingIHave}");
            tallGuy.Honk();
        }
    }
}
