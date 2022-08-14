using System.Text.Json;

namespace SerializeGuys {
    internal class Program {
        static void Main( string[] args ) {
            var guys = new List<Guy>() {
                new Guy() { Name = "Bob", Clothes = new Outfit() { Top = "t-shirt", Bottom = "jeans" },Hair = new HairStyle() { Color = HairColor.Red, Length = 3.5f }},
                new Guy() { Name = "Joe", Clothes = new Outfit() { Top = "polo", Bottom = "slacks" }, Hair = new HairStyle() { Color = HairColor.Gray, Length = 2.7f }},
            };

            //var jsonString = JsonSerializer.Serialize(guys);
            var options = new JsonSerializerOptions() { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(guys, options);
            Console.WriteLine(jsonString);

            var copyOfGuys = JsonSerializer.Deserialize<List<Guy>>(jsonString);
            foreach (var guy in copyOfGuys)
                Console.WriteLine("I deserialized this guy: {0}", guy);
        }
    }
}