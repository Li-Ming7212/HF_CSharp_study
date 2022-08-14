using System;
namespace GoFishTests {
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using GoFish;
    using System.Linq;

    [TestClass]
    public class GameControllerTests {
        [TestInitialize]
        public void Initialize() {
            Player.Random = new MockRandom() { ValueToReturn = 0 };
        }


        [TestMethod]
        public void TestConstructor() {
            var gameController = new GameController("Human",
                            new List<string>() { "Player1", "Player2", "Player3" });
            Assert.AreEqual("开始游戏，玩家为 Human, Player1, Player2, Player3",
                            gameController.Status);
        }

        [TestMethod]
        public void TestNextRound() {
            // The constructor shuffles the deck, but MockRandom makes sure it stays in order
            // so Owen should have Ace to 5 of Diamonds, Brittney should have 6 to 10 of Diamonds
            var gameController = new GameController("Owen", new List<string>() { "Brittney" });

            gameController.NextRound(gameController.Opponents.First(), Values.Six);
            Assert.AreEqual("Owen向Brittney问道，有Six吗？" +
             Environment.NewLine + "Brittney有1张Six牌" +
             Environment.NewLine + "Brittney向Owen问道，有Seven吗？" +
             Environment.NewLine + "Brittney从牌库抽了一张牌" +
             Environment.NewLine + "Owen有6张手牌，已经集齐0组" +
             Environment.NewLine + "Brittney有5张手牌，已经集齐0组" +
             Environment.NewLine + "牌库有41张牌" +
             Environment.NewLine, gameController.Status);
            
        }

        [TestMethod]
        public void TestNewGame() {
            Player.Random = new MockRandom() { ValueToReturn = 0 };
            var gameController = new GameController("Owen", new List<string>() { "Brittney" });
            gameController.NextRound(gameController.Opponents.First(), Values.Six);
            gameController.NewGame();
            Assert.AreEqual("Owen", gameController.HumanPlayer.Name);
            Assert.AreEqual("Brittney", gameController.Opponents.First().Name);
            Assert.AreEqual("开始新游戏", gameController.Status);
        }
    }
}
