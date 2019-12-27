
using NUnit.Framework;

namespace Tennis.Common.Unit.Tests
{
    [TestFixture()]
    public class ProcessorFixture
    {
        PointsProcessor pointsProcessor;

        [SetUp]
        public void SetUp()
        {
            pointsProcessor = new PointsProcessor();
        }

        [Test()]
        public void GIVEN_test_file_line_1()
        {
            // Arrange
            string points = string.Empty;

            // Act
            Match match = pointsProcessor.Process(points);

            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_2()
        {
            // Arrange
            string points = "A";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 15-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_3()
        {
            // Arrange
            string points = "AA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 30-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_4()
        {
            // Arrange
            string points = "AAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 40-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_5()
        {
            // Arrange
            string points = "B";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 0-15", result);
        }

        [Test()]
        public void GIVEN_test_file_line_6()
        {
            // Arrange
            string points = "BB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 0-30", result);
        }

        [Test()]
        public void GIVEN_test_file_line_7()
        {
            // Arrange
            string points = "BBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 0-40", result);
        }

        [Test()]
        public void GIVEN_test_file_line_8()
        {
            // Arrange
            string points = "BBBA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 15-40", result);
        }

        [Test()]
        public void GIVEN_test_file_line_9()
        {
            // Arrange
            string points = "BBBAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 30-40", result);
        }

        [Test()]
        public void GIVEN_test_file_line_10()
        {
            // Arrange
            string points = "BBBAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 40-40", result);
        }

        [Test()]
        public void GIVEN_test_file_line_11()
        {
            // Arrange
            string points = "BBBAAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 A-40", result);
        }

        [Test()]
        public void GIVEN_test_file_line_12()
        {
            // Arrange
            string points = "BBBAAAAB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 40-40", result);
        }

        [Test()]
        public void GIVEN_test_file_line_13()
        {
            // Arrange
            string points = "BBBAAAABB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-0 40-A", result);
        }

        [Test()]
        public void GIVEN_test_file_line_14()
        {
            // Arrange
            string points = "AAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-1", result);
        }

        [Test()]
        public void GIVEN_test_file_line_15()
        {
            // Arrange
            string points = "BBBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("1-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_16()
        {
            // Arrange
            string points = "BBBAAAABBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("1-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_17()
        {
            // Arrange
            string points = "AAAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-1 0-15", result);
        }

        [Test()]
        public void GIVEN_test_file_line_18()
        {
            // Arrange
            string points = "AAAAABB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("0-1 30-15", result);
        }

        [Test()]
        public void GIVEN_test_file_line_19()
        {
            // Arrange
            string points = "AAAABBBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("1-1", result);
        }

        [Test()]
        public void GIVEN_test_file_line_20()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("2-2", result);
        }

        [Test()]
        public void GIVEN_test_file_line_21()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("3-3", result);
        }

        [Test()]
        public void GIVEN_test_file_line_22()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("5-5", result);
        }

        [Test()]
        public void GIVEN_test_file_line_23()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBB";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("6-6", result);
        }

        [Test()]
        public void GIVEN_test_file_line_24()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("6-4 0-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_25()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("7-5 0-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_27()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("6-4 0-0 15-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_29()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAAAAAAAAAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("3-6 0-0 0-15", result);
        }

        [Test()]
        public void GIVEN_test_file_line_30()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAAA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("7-5 0-0 15-0", result);
        }

        [Test()]
        public void GIVEN_test_file_line_31()
        {
            // Arrange
            string points = "AAAABBBBAAAABBBBAAAABBBBAAAAAAAAAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBBBBBA";

            // Act
            Match match = pointsProcessor.Process(points);
            string result = match.ScoringSystem.MatchScore();

            // Assert
            Assert.AreEqual("3-6 6-4 0-0 0-15", result);
        }
    }
}
