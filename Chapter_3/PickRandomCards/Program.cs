using System;

namespace PickRandomCards {
    class Program {
        static void Main(string[] args) {
            Console.Write("你要几张牌？张数: ");
            string line = Console.ReadLine();

            if (int.TryParse(line, out int numberOfCards)) {
                foreach (string card in CardPicker.PickSomeCards(numberOfCards)) {
                    Console.WriteLine(card);
                }
            }
            else {
                Console.WriteLine("请输入正确的数字.");
            }
        }
    }
}





