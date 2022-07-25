using System;

namespace JumbledCards {
    internal class Program {

        private static readonly Random random = new Random();
        static Card RandomCard() {
            return new Card((Values)random.Next(1, 14), (Suits)random.Next(4));
        }

        static void PrintCards(List<Card> cards) {
            foreach (Card card in cards) {
                Console.WriteLine(card.Name);
            }
        }

        static void Main(string[] args) {

            //Card myCard = new Card((Values)random.Next(1, 14), (Suits)random.Next(4));
            //Console.WriteLine(myCard.Name);

            List<Card> cards = new List<Card>();
            Console.Write("卡牌数量: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfCards))
                for (int i = 0; i < numberOfCards; i++)
                    cards.Add(RandomCard());

            PrintCards(cards);

            cards.Sort(new CardComparerByValue());
            Console.WriteLine("\n... 排卡中 ...\n");

            PrintCards(cards);
        }
    }
}
