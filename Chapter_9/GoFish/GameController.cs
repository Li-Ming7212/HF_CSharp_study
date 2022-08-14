using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish {
    public class GameController {
        public static Random Random = new Random();
        private GameState gameState;
        public bool GameOver { get { return gameState.GameOver; } }
        public Player HumanPlayer { get { return gameState.HumanPlayer; } }
        public IEnumerable<Player> Opponents { get { return gameState.Opponents; } }
        public string Status { get; private set; }
        /// <summary>
        /// 游戏构造器
        /// </summary>
        /// <param name="humanPlayerName">玩家名字</param>
        /// <param name="computerPlayerNames">电脑名字</param>
        public GameController( string humanPlayerName, IEnumerable<string> computerPlayerNames ) {
            gameState = new GameState(humanPlayerName, computerPlayerNames, new Deck().Shuffle());
            Status = $"开始游戏，玩家为 {string.Join(", ", gameState.Players)}";
        }
        /// <summary>
        /// 进行下一轮，如果所有人的牌都用完了就结束游戏
        /// </summary>
        /// <param name="playerToAsk">玩家要牌的那个人</param>
        /// <param name="valueToAskFor">要的数字</param>
        public void NextRound( Player playerToAsk, Values valueToAskFor ) {
            Status = gameState.PlayRound(gameState.HumanPlayer, playerToAsk, valueToAskFor, gameState.Stock) + Environment.NewLine;

            ComputerPlayersPlayNextRound();

            Status += string.Join(Environment.NewLine, gameState.Players.Select(player => player.Status));
            Status += $"{Environment.NewLine}牌库有{gameState.Stock.Count()}张牌";
            Status += Environment.NewLine + gameState.CheckForWinner();
        }
        /// <summary>
        /// 所有有牌的电脑玩家都会玩下一轮。如果人类的牌用完了，那么牌堆就会耗尽，他们将玩完游戏的其余部分。
        /// </summary>
        private void ComputerPlayersPlayNextRound() {
            IEnumerable<Player> computerPlayersWithCards;
            do {
                computerPlayersWithCards =
                gameState
                .Opponents
                .Where(player => player.Hand.Count() > 0);
                foreach (Player player in computerPlayersWithCards) {
                    var randomPlayer = gameState.RandomPlayer(player);
                    var randomValue = player.RandomValueFromHand();
                    Status += gameState
                    .PlayRound(player, randomPlayer, randomValue, gameState.Stock)
                    + Environment.NewLine;
                }
            } while ((gameState.HumanPlayer.Hand.Count() == 0)
            && (computerPlayersWithCards.Count() > 0));
        }
        /// <summary>
        /// 用相同的玩家名字开始新的游戏
        /// </summary>
        public void NewGame() {
            Status = "开始新游戏";
            gameState = new GameState(gameState.HumanPlayer.Name,
            gameState.Opponents.Select(player => player.Name),
            new Deck().Shuffle());
        }
    }
}
