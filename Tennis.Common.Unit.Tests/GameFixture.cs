using NUnit.Framework;
using System;
using Tennis.Contracts;
using Tennis.Common.ScoreState;

namespace Tennis.Common.Unit.Tests
{
    [TestFixture()]
    public class GameFixture
    {
        private Player playerA;
        private Player playerB;
        private ScoreFormatter scoreFormatter;
        private ScoringSystem scoreSystem;
        private Match match;

        [SetUp]
        public void SetUp()
        {
            playerA = new Player("Fred Perry");
            playerB = new Player("Arthur Ashe");
            scoreFormatter = new ScoreFormatter();
            scoreSystem = new ScoringSystem(playerA, playerB, scoreFormatter);
            match = new Match(playerA, playerB, scoreSystem);
        }

        [Test()]
        public void WhenGameIsPassedNullMatchThenArgumentNullExceptionIsThrown()
        {
            // Act, Assert
            try
            {
                Game sut = new Game(null);
                Assert.Fail("Should throw ArgumentNullException");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(ex.ParamName, "match");
            }
        }

        [Test()]
        public void TestGameStateIsNormalWhenGameStarted()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);

            // Assert
            Assert.IsInstanceOf<PointScoringState>(game.Match.ScoringSystem.State);
        }

        [Test()]
        public void TestGameStateIsNormalWhenPlayerAScoreAPoint()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);

            // Assert
            Assert.IsInstanceOf<PointScoringState>(game.Match.ScoringSystem.State);
        }

        [Test()]
        public void TestGameStateIsDeuceWhenBothPlayersHaveScored4Points()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);

            // Assert
            Assert.IsInstanceOf<DeuceScoringState>(game.Match.ScoringSystem.State);
        }

        [Test()]
        public void TestGameStateIsGameOverWhenPlayerAHasScored4Points()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerA);
            game.ScorePoint(playerA);
            game.ScorePoint(playerA);

            // Assert
            Assert.IsInstanceOf<GameOverState>(game.Match.ScoringSystem.State);
        }

        [Test()]
        public void TestGameStateIsAdvantageForPlayerA()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);

            // take game to advantage player A
            game.ScorePoint(playerA);

            // Assert
            Assert.IsInstanceOf<AdvantageScoringState>(game.Match.ScoringSystem.State);
            Assert.AreEqual(game.Match.PlayerA.Advantage, true);
            Assert.AreEqual(game.Match.PlayerB.Advantage, false);
        }

        [Test()]
        public void TestGameStateIsDeuceWhenPlayerAHasAdvantageAndPlayerBScoresAPoint()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);

            // take game to advantage player A
            game.ScorePoint(playerA);
            // take game back to deuce 
            game.ScorePoint(playerB);

            // Assert
            Assert.IsInstanceOf<DeuceScoringState>(game.Match.ScoringSystem.State);
            Assert.AreEqual(game.Match.PlayerA.Advantage, false);
            Assert.AreEqual(game.Match.PlayerB.Advantage, false);
        }

        [Test()]
        public void TestGameStateIsGameOverWhenPlayerAHasAdvantageAndPlayerAScoresAPoint()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);

            // take game to advantage player A
            game.ScorePoint(playerA);
            // game won for player A
            game.ScorePoint(playerA);

            // Assert
            Assert.IsInstanceOf<GameOverState>(game.Match.ScoringSystem.State);
            Assert.AreEqual(game.Match.PlayerA.Advantage, false);
            Assert.AreEqual(game.Match.PlayerB.Advantage, false);
        }

        [Test()]
        public void TestGameScoreCorrectWhenNoPlayerHasScored()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);

            // Assert
            Assert.AreEqual("0-0", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreCorrectWhenPlayerAIsServingAndScoresAPoint()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);

            // Assert
            Assert.AreEqual("0-0 15-0", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreIsCorrectWhenPlayerAIsServingAndScoresTwoPoints()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerA);

            // Assert
            Assert.AreEqual("0-0 30-0", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreIsCorrectWhenPlayerAIsServingAndScoresThreePoints()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerA);
            game.ScorePoint(playerA);

            // Assert
            Assert.AreEqual("0-0 40-0", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreCorrectWhenPlayerBIsNotServingAndScoresAPoint()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerB);

            // Assert
            Assert.AreEqual("0-0 0-15", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreIsCorrectWhenPlayerBIsNotServingAndScoresTwoPoints()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            // Assert
            Assert.AreEqual("0-0 0-30", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreIsCorrectWhenPlayerBIsNotServingAndScoresThreePoints()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            // Assert
            Assert.AreEqual("0-0 0-40", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreIsCorrectWhenPlayersAtDeuce()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            // deuce
            game.ScorePoint(playerB);

            // Assert
            Assert.AreEqual("0-0 40-40", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameScoreIsCorrectWhenPlayerAIsAtAdvantage()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            // advantage
            game.ScorePoint(playerA);

            // Assert
            Assert.AreEqual("0-0 A-40", game.Match.ScoringSystem.MatchScore());
        }


        [Test()]
        public void TestGameSetScoreIsCorrectWhenPlayerAWinsOneGame()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            // won
            game.ScorePoint(playerA);

            // Assert
            //Assert.AreEqual("0-0", game.GetScore());
            Assert.AreEqual(1, game.Match.ScoringSystem.GetCurrentSetScore(playerA));
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerB));
        }

        [Test()]
        public void TestGameSetScoreIsCorrectWhenPlayerAWinsOneGameAndWinsTwoPoints()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            // won
            game.ScorePoint(playerA);

            game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerA);

            // Assert
            Assert.AreEqual("0-1 0-30", game.Match.ScoringSystem.MatchScore());
            Assert.AreEqual(1, game.Match.ScoringSystem.GetCurrentSetScore(playerA));
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerB));
        }

        [Test()]
        public void TestGameSetScoreIsCorrectWhenPlayerAWinsTwoGames()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            // won
            game.ScorePoint(playerA);

            game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            // won
            game.ScorePoint(playerA);

            // Assert
            Assert.AreEqual("2-0", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameSetScoreIsCorrectWhenPlayerAWinsTwoGamesAndPlayerBWinsGame()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            // won
            game.ScorePoint(playerA);

            game = new Game(match);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            game.ScorePoint(playerB);
            game.ScorePoint(playerA);
            // won
            game.ScorePoint(playerA);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            // won
            game.ScorePoint(playerB);

            // Assert
            Assert.AreEqual("1-2", game.Match.ScoringSystem.MatchScore());
        }

        [Test()]
        public void TestGameSetScoreIsCorrectWhenPlayerBWinsOneSetBySixGames()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            // Assert
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerA));
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerB));
            Assert.AreEqual(0, game.Match.ScoringSystem.CompletedSets[0].PlayerAScore);
            Assert.AreEqual(6, game.Match.ScoringSystem.CompletedSets[0].PlayerBScore);
        }

        [Test()]
        public void TestGameSetScoreIsCorrectWhenPlayerBWinsTwoSets()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            // set 2

            // Act
            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            // Assert
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerA));
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerB));

            Assert.AreEqual(0, game.Match.ScoringSystem.CompletedSets[0].PlayerAScore);
            Assert.AreEqual(6, game.Match.ScoringSystem.CompletedSets[0].PlayerBScore);

            Assert.AreEqual(0, game.Match.ScoringSystem.CompletedSets[1].PlayerAScore);
            Assert.AreEqual(6, game.Match.ScoringSystem.CompletedSets[1].PlayerBScore);
        }

        [Test()]
        public void TestGameSetScoreIsCorrectWhenPlayerBWinsThreeSets()
        {
            // Arrange
            match.ScoringSystem.Server = playerA;

            // Act
            Game game = new Game(match);
            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            // set 2

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            // set 3
            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);

            game = new Game(match);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);
            game.ScorePoint(playerB);


            // Assert
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerA));
            Assert.AreEqual(0, game.Match.ScoringSystem.GetCurrentSetScore(playerB));

            Assert.AreEqual(0, game.Match.ScoringSystem.CompletedSets[0].PlayerAScore);
            Assert.AreEqual(6, game.Match.ScoringSystem.CompletedSets[0].PlayerBScore);

            Assert.AreEqual(0, game.Match.ScoringSystem.CompletedSets[1].PlayerAScore);
            Assert.AreEqual(6, game.Match.ScoringSystem.CompletedSets[1].PlayerBScore);

            Assert.AreEqual(0, game.Match.ScoringSystem.CompletedSets[2].PlayerAScore);
            Assert.AreEqual(6, game.Match.ScoringSystem.CompletedSets[2].PlayerBScore);
        }
    }
}
