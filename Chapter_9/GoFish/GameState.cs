using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish {
    public class GameState {
        public readonly IEnumerable<Player> Players;//玩家
        public readonly IEnumerable<Player> Opponents;//对手
        public readonly Player HumanPlayer;
        public bool GameOver { get; private set; } = false;
        public readonly Deck Stock;
        /// <summary>
        /// 构造器开始玩家们的第一回合
        /// </summary>
        /// <param name="humanPlayerName">玩家</param>
        /// <param name="opponentNames">电脑</param>
        /// <param name="stock">牌库</param>
        public GameState( string humanPlayerName, IEnumerable<string> opponentNames, Deck stock ) {
            this.Stock = stock;

            this.HumanPlayer = new Player(humanPlayerName);
            HumanPlayer.GetNextHand(stock);

            var opponents = new List<Player>();
            foreach (var name in opponentNames) {
                var player = new Player(name);
                player.GetNextHand(stock);
                opponents.Add(player);
            }
            Opponents = opponents;

            Players = new List<Player>() { HumanPlayer }.Concat(opponents);
        }
        /// <summary>
        /// 随机匹配一名不是当前玩家的玩家
        /// </summary>
        /// <param name="currentPlayer">当前玩家</param>
        /// <returns>当前玩家可以要求一张牌的随机玩家</returns>
        public Player RandomPlayer( Player currentPlayer ) =>
        Players.Where(player => player != currentPlayer)
            .Skip(Player.Random.Next(Players.Count() - 1))
            .First();

        /// <summary>
        /// 使一名玩家进行一回合
        /// </summary>
        /// <param name="player">当前玩家</param>
        /// <param name="playerToAsk">被要求出示一张牌的玩家</param>
        /// <param name="valueToAskFor">要牌的数字</param>
        /// <param name="stock">牌库</param>
        /// <returns>描述发生的事情</returns>
        public string PlayRound( Player player, Player playerToAsk, Values valueToAskFor, Deck stock ) {

            //var valuePlural = (valueToAskFor == Values.Six) ? "Sixes" : $"{valueToAskFor}s";
            var message = $"{player.Name}向{playerToAsk.Name}问道，有{valueToAskFor}吗？{Environment.NewLine}";

            var cards = playerToAsk.DoYouHaveAny(valueToAskFor, stock);
            if (cards.Count() > 0) {
                player.AddCardsAndPullOutBooks(cards);
                message += $"{playerToAsk.Name}有{cards.Count()}张{valueToAskFor}牌";
            }
            else if (stock.Count == 0) {
                message += "牌库已空";
            }
            else {
                player.DrawCard(stock);
                message += $"{player.Name}从牌库抽了一张牌";
            }

            if (player.Hand.Count() == 0) {
                player.GetNextHand(stock);
                message += $"{Environment.NewLine}{player.Name} 手牌已空，从牌库抽{player.Hand.Count()}张牌";

            }

            return message;
        }
        /// <summary>
        /// 通过查看是否有任何玩家剩下任何牌来检查赢家，如果游戏结束并且有赢家，则设置GameOver
        /// </summary>
        /// <returns>包含赢家的字符串，如果没有赢家，则为空字符串</returns>
        public string CheckForWinner() {
            var playerCards = Players.Select(player => player.Hand.Count()).Sum();
            if (playerCards > 0)
                return "";
            GameOver = true;
            var winningBookCount = Players.Select(player => player.Books.Count()).Max();
            var winners = Players.Where(player => player.Books.Count() == winningBookCount);
            if (winners.Count() == 1)
                return $"获胜者是{winners.First().Name}";
            return $"获胜者是{string.Join("和", winners)}";
        }
    }
}
