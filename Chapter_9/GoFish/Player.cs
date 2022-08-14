using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish {
    public class Player {

        public static Random Random = new Random();
        private List<Card> hand = new List<Card>();
        private List<Values> books = new List<Values>();

        /// <summary>
        /// 在玩家手上的卡
        /// </summary>
        public IEnumerable<Card> Hand => hand;

        /// <summary>
        ///玩家集齐并打出的一组卡
        /// </summary>
        public IEnumerable<Values> Books => books;

        public readonly string Name;

        /// <summary>
        /// 将单词复数，如果值不等于1，则添加“s”
        /// </summary>
        public static string S(int s) => s == 1 ? "" : "s";

        /// <summary>
        /// 返回玩家的当前状态：手牌和Books的数量
        /// </summary>
        public string Status => $"{Name}有{hand.Count}张手牌，已经集齐{books.Count}组";

        /// <summary>
        /// 创建Player的构造函数
        /// </summary>
        /// <param name="name">玩家姓名</param>
        public Player(string name) => Name = name;

        /// <summary>
        /// 替代构造函数（用于单元测试）
        /// </summary>
        /// <param name="name">玩家姓名</param>
        /// <param name="cards">初始卡片集</param>
        public Player(string name, IEnumerable<Card> cards) {
            Name = name;
            hand.AddRange(cards);
        }


        /// <summary>
        /// 从库存中获取最多五张卡
        /// </summary>
        /// <param name="stock">牌库</param>
        public void GetNextHand(Deck stock) {
            while ((stock.Count() > 0) && (hand.Count < 5)) {
                hand.Add(stock.Deal(0));
            }
        }


        /// <summary>
        /// 如果我有任何与值匹配的卡，请将其退回。如果我的牌用完了，就从牌组中换下一手牌。
        /// </summary>
        /// <param name="value">呼唤的数字</param>
        /// <param name="deck">牌库</param>
        /// <returns>从另一个玩家手中抽出的牌</returns>
        public IEnumerable<Card> DoYouHaveAny(Values value, Deck deck) {
            var matchingCards = hand.Where(card => card.Value == value).OrderBy(Card => Card.Suit).ToList();
            hand = hand.Where(card => card.Value != value).ToList();

            if (hand.Count == 0) {
                GetNextHand(deck);
            }

            return matchingCards;
        }
        /// <summary>
        /// 当玩家收到另一个玩家的牌时，将其添加到手上并拉出任何匹配的books
        /// </summary>
        /// <param name="cards">要添加的其他玩家的牌</param>
        public void AddCardsAndPullOutBooks(IEnumerable<Card> cards) {
            hand.AddRange(cards);

            var foundbooks =
                hand.GroupBy(card => card.Value)
                .Where(group => group.Count() == 4)
                .Select(group => group.Key);

            books.AddRange(foundbooks);
            books.Sort();

            hand = hand
                .Where(card => !books.Contains(card.Value))
                .ToList();
        }
        /// <summary>
        /// 从牌堆中抽出一张牌，并将其添加到玩家手中
        /// </summary>
        /// <param name="stock">剩余牌堆</param>
        public void DrawCard(Deck stock) {
            if (stock.Count > 0) {
                AddCardsAndPullOutBooks(new List<Card> { stock.Deal(0) });
            }
        }
        /// <summary>
        /// 从玩家手上获取随机值
        /// </summary>
        /// <returns>The value of a randomly selected card in the player's hand</returns>
        public Values RandomValueFromHand() => hand
            .OrderBy(card => card.Value)
             .Select(card => card.Value)
             .Skip(Random.Next(hand.Count()))
             .First();
        public override string ToString() => Name;


    }
}
