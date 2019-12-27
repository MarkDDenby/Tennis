using NUnit.Framework;
using System;

namespace Tennis.Common.Unit.Tests
{
    [TestFixture()]
    public class PlayerFixture
    {
        [Test()]
        public void WhenPlayerIsPassedEmptyNameThenArgumentNullExceptionIsThrown()
        {
            try
            {
                Player sut = new Player(string.Empty);
                Assert.Fail("Should throw ArgumentNullException");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(ex.ParamName, "name");
            }
        }

        [Test()]
        public void WhenPlayerIsPassedValidNameThenPlayerNameIsCorrect()
        {
            // Arrange
            String expectedResult = "Fred Perry";

            // Act
            Player sut = new Player("Fred Perry");

            // Assert
            Assert.AreEqual(expectedResult, sut.Name);
        }

        [Test()]
        public void WhenPlayerAdvantageSetThenPlayerAdvantageIsCorrect()
        {
            // Arrange
            Boolean expectedResult = true;

            // Act
            Player sut = new Player("Fred Perry");
            sut.Advantage = true;

            // Assert
            Assert.AreEqual(expectedResult, sut.Advantage);
        }
    }
}
