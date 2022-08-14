using System;
namespace GoFish {
    using System.Collections.Generic;

    public class Deck : List<Card> {
        private static Random random = Player.Random;
        //private static Random random = new Random();

        public Deck() {
            Reset();
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
        public Card Deal(int index) {
            Card cardToDeal = base[index];
            RemoveAt(index);
            return cardToDeal;
        }
    }

}
