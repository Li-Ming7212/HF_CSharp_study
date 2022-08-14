namespace UseABinaryWriter {
    using System.IO;
    internal class Program {
        static void Main( string[] args ) {
            int intValue = 487611223;
            string stringValue = "Hello!";
            byte[] byteArray = { 47, 129, 0, 116 };
            float floatValue = 491.22f;
            char charValue = 'E';

            using (var output = File.Create("binarydata.dat"))
            using (var writer = new BinaryWriter(output)) {
                writer.Write(intValue);
                writer.Write(stringValue);
                writer.Write(byteArray);
                writer.Write(floatValue);
                writer.Write(charValue);
            }

            byte[] dataWritten = File.ReadAllBytes("binarydata.dat");
            foreach (byte b in dataWritten)
                Console.Write($"{b:x2} ");
            Console.WriteLine($" - {dataWritten.Length} bytes");

            using (var input = File.OpenRead("binarydata.dat"))
            using (var reader = new BinaryReader(input)) {
                int intRead = reader.ReadInt32();
                string stringRead = reader.ReadString();
                byte[] byteArrayRead = reader.ReadBytes(4);
                float floatRead = reader.ReadSingle();
                char charRead = reader.ReadChar();

                Console.Write($"int: {intRead}  string: {stringRead}  bytes: ");
                foreach (byte b in byteArrayRead)
                    Console.Write($"{b} ");
                Console.Write($" float: {floatRead}  char: {charRead} ");
            }



        }
    }
}