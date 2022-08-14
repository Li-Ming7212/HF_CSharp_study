using System;
using System.Collections.Generic;
using System.Text;

namespace WriteADeck {
    /// <summary>
    /// 数字
    /// </summary>
    public enum Values {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }
    /// <summary>
    /// 花色
    /// </summary>
    public enum Suits {
        Diamonds,
        Clubs,
        Hearts,
        Spades,
    }
    public class Card {
        public Values Value { get; private set; }
        public Suits Suit { get; private set; }

        public Card(Values value, Suits suit) {
            this.Suit = suit;
            this.Value = value;
        }
        public string Name {
            get { return $"{Value} of {Suit}"; }
        }

        public override string ToString() {
            return Name;
        }

    }

}
