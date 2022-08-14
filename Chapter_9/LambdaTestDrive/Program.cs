namespace LambdaTestDrive {
    internal class Program {

        static Random random => new Random();
        static double GetRandomDouble(int max) => random.NextDouble()* max;

        static void PrintValue(double d) => Console.WriteLine($"the value is {d:0.000}");

        static void Main(string[] args) {
            var value = GetRandomDouble(100);
            PrintValue(value);
        }
    }
}