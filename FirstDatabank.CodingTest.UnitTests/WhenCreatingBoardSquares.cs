namespace FirstDatabank.CodingTest.UnitTests
{
    using GamePlay;
    using NUnit.Framework;

    [TestFixture]
    public class WhenCreatingBoardSquares
    {
        [Test]
        public void ItCreatesWithNoLandmine()
        {
            // arrange

            // act
            var noLandmine = new BoardSquare(false, false);

            // assert
            Assert.IsFalse(noLandmine.IsLandmine, "There should not be a landmine here");
        }

        [Test]
        public void ItCreatesWithLandmine()
        {
            // arrange

            // act
            var noLandmine = new BoardSquare(true, false);

            // assert
            Assert.IsTrue(noLandmine.IsLandmine, "There should be a landmine here");
        }

        [Test]
        public void ItCreatesAsVisited()
        {
            // arrange

            // act
            var noLandmine = new BoardSquare(false, true);

            // assert
            Assert.IsTrue(noLandmine.IsVisited, "This should be visited here");
        }

        [Test]
        public void ItCreatesAsUnvisited()
        {
            // arrange

            // act
            var noLandmine = new BoardSquare(false, false);

            // assert
            Assert.IsFalse(noLandmine.IsVisited, "This should not be visited here");
        }

    }
}
