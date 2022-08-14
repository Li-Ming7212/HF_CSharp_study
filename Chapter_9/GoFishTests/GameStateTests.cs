using System;
namespace GoFishTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using GoFish;

    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };
            var gameState = new GameState("Human", computerPlayerNames, new Deck());

            CollectionAssert.AreEqual(
                new List<string> { "Human", "Computer1", "Computer2", "Computer3" },
                gameState.Players.Select(player => player.Name).ToList());

            Assert.AreEqual(5, gameState.HumanPlayer.Hand.Count());
        }


        [TestMethod]
        public void TestRandomPlayer()
        {
            var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };

            var gameState = new GameState("Human", computerPlayerNames, new Deck());
            Player.Random = new MockRandom() { ValueToReturn = 1 };
            Assert.AreEqual("Computer2",gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);

            Player.Random = new MockRandom() { ValueToReturn = 0 };
            Assert.AreEqual("Human", gameState.RandomPlayer(gameState.Players.ToList()[1]).Name);
            Assert.AreEqual("Computer1", gameState.RandomPlayer(gameState.Players.ToList()[0]).Name);
        }

        [TestMethod]
        public void TestPlayRound()
        {
            var deck = new Deck();
            deck.Clear();
            var cardsToAdd = new List<Card>() {

            // Cards the game will deal to Owen
            new Card(Values.Jack, Suits.Spades),
            new Card(Values.Jack, Suits.Hearts),
            new Card(Values.Six, Suits.Spades),
            new Card(Values.Jack, Suits.Diamonds),
            new Card(Values.Six, Suits.Hearts),

            // Cards the game will deal to Brittney
            new Card(Values.Six, Suits.Diamonds),
            new Card(Values.Six, Suits.Clubs),
            new Card(Values.Seven, Suits.Spades),
            new Card(Values.Jack, Suits.Clubs),
            new Card(Values.Nine, Suits.Spades),

            // Two more cards in the deck for Owen to draw when he runs out
            new Card(Values.Queen, Suits.Hearts),
            new Card(Values.King, Suits.Spades),
        };

            foreach (var card in cardsToAdd)
                deck.Add(card);

            var gameState = new GameState("Owen", new List<string>() { "Brittney" }, deck);

            var owen = gameState.HumanPlayer;
            var brittney = gameState.Opponents.First();

            Assert.AreEqual("Owen", owen.Name);
            Assert.AreEqual(5, owen.Hand.Count());
            Assert.AreEqual("Brittney", brittney.Name);
            Assert.AreEqual(5, brittney.Hand.Count());

            var message = gameState.PlayRound(owen, brittney, Values.Jack, deck);
            Assert.AreEqual("Owen向Brittney问道，有Jack吗？" + Environment.NewLine + "Brittney有1张Jack牌", message);
            Assert.AreEqual(1, owen.Books.Count());
            Assert.AreEqual(2, owen.Hand.Count());
            Assert.AreEqual(0, brittney.Books.Count());
            Assert.AreEqual(4, brittney.Hand.Count());

            message = gameState.PlayRound(brittney, owen, Values.Six, deck);
            Assert.AreEqual("Brittney向Owen问道，有Six吗？" + Environment.NewLine + "Owen有2张Six牌", message);
            Assert.AreEqual(1, owen.Books.Count());
            Assert.AreEqual(2, owen.Hand.Count());
            Assert.AreEqual(1, brittney.Books.Count());
            Assert.AreEqual(2, brittney.Hand.Count());

            message = gameState.PlayRound(owen, brittney, Values.Queen, deck);
            Assert.AreEqual("Owen向Brittney问道，有Queen吗？" + Environment.NewLine + "牌库已空", message);
            Assert.AreEqual(1, owen.Books.Count());
            Assert.AreEqual(2, owen.Hand.Count());
        }

        [TestMethod]
        public void TestCheckForAWinner()
        {
            var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3",
        };

            var emptyDeck = new Deck();
            emptyDeck.Clear();
            var gameState = new GameState("Human", computerPlayerNames, emptyDeck);
            Assert.AreEqual("获胜者是Human和Computer1和Computer2和Computer3",
                            gameState.CheckForWinner());
        }
    }
}
