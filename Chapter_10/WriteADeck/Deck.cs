using System;
namespace WriteADeck {
    using System.Collections.Generic;
    using System.IO;

    public class Deck : List<Card> {
        //private static Random random = Player.Random;
        private static Random random = new Random();

        public Deck() {
            Reset();
        }
        public Deck( string filename ) {
            using (var sr = new StreamReader(filename)) {
                while (!sr.EndOfStream) {
                    var nextCard = sr.ReadLine();
                    var cardParts = nextCard.Split(new char[] { ' ' });
                    var value = cardParts[0] switch {
                        "Ace" => Values.Ace,
                        "Two" => Values.Two,
                        "Three" => Values.Three,
                        "Four" => Values.Four,
                        "Five" => Values.Five,
                        "Six" => Values.Six,
                        "Seven" => Values.Seven,
                        "Eight" => Values.Eight,
                        "Nine" => Values.Nine,
                        "Ten" => Values.Ten,
                        "Jack" => Values.Jack,
                        "Queen" => Values.Queen,
                        "King" => Values.King,
                        _ => throw new InvalidDataException($"无法识别的卡值: {cardParts[0]}")
                    };
                    var suit = cardParts[2] switch {
                        "Spades" => Suits.Spades,
                        "Clubs" => Suits.Clubs,
                        "Hearts" => Suits.Hearts,
                        "Diamonds" => Suits.Diamonds,
                        _ => throw new InvalidDataException($"无法识别的花色: {cardParts[2]}"),
                    };
                    Add(new Card(value, suit));
                }
            }
        }

        /// <summary>
        /// 重设牌组（顺序编排）
        /// </summary>
        public void Reset() {
            Clear();
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 13; value++)
                    Add(new Card((Values)value, (Suits)suit));
        }
        /// <summary>
        /// 洗牌
        /// </summary>
        /// <returns>洗好的卡组</returns>
        public Deck Shuffle() {
            List<Card> copy = new List<Card>(this);
            Clear();
            while (copy.Count > 0) {
                int index = random.Next(copy.Count);
                Card card = copy[index];
                copy.RemoveAt(index);
                Add(card);
            }
            return this;
        }
        /// <summary>
        /// 根据下标从卡组取牌
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>从卡组取出的牌</returns>
        public Card Deal( int index ) {
            Card cardToDeal = base[index];
            RemoveAt(index);
            return cardToDeal;
        }

        public void WriteCards( string fileName ) {
            using (var sw = new StreamWriter(fileName)) {
                for (int i = 0; i < Count; i++) {
                    sw.WriteLine(this[i].Name);
                }
            }
        }
    }

}
