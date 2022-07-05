using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickACardUI {
    internal class CardPicker {
        static Random random = new Random();
        public static string[] PickSomeCards(int numberOfCards) {
            string[] pickedCards = new string[numberOfCards];
            for (int i = 0; i < numberOfCards; i++) {
                pickedCards[i] = RandomSuit() + " " + RandomValue();
            }
            return pickedCards;
        }

        private static string RandomSuit() {
            int value = random.Next(1, 5);
            if (value == 1) return "黑桃";
            if (value == 2) return "红心";
            if (value == 3) return "梅花";
            return "方块";
        }

        private static string RandomValue() {
            int value = random.Next(1, 14);
            if (value == 1) return "Ace";
            if (value == 11) return "Jack";
            if (value == 12) return "Queen";
            if (value == 13) return "King";
            return value.ToString();
        }
    }
}
