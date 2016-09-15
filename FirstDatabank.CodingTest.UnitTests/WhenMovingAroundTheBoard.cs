namespace FirstDatabank.CodingTest.UnitTests
{
    using System.Drawing;
    using Core;
    using GamePlay;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class WhenMovingAroundTheBoard
    {
        private const int BOARD_ROWS = 8;
        private const int BOARD_COLUMNS = 7;

        private Mock<IRandomGenerator> _mockRandom;
        private ISquareFactory _squareFactory;
        private IMinefieldBoard _board;

        [TestFixtureSetUp]
        public void CreateBoardDependencies()
        {
            // always return false when generating bools
            _mockRandom = new Mock<IRandomGenerator>();
            _mockRandom.Setup(r => r.GenerateBool(It.IsAny<float>())).Returns(false);

            // use the actual square factory - this is unit tested so we can trust it
            _squareFactory = new SquareFactory(_mockRandom.Object);

            // use the actual board to unit test
            _board = new MinefieldBoard(_squareFactory);
            _board.Initialise(BOARD_ROWS, BOARD_COLUMNS);
        }

        [TestCase(0, 0)] [TestCase(0, 1)] [TestCase(0, 2)] [TestCase(0, 3)] [TestCase(0, 4)] [TestCase(0, 5)] [TestCase(0, 6)] [TestCase(0, 7)]
        [TestCase(1, 0)] [TestCase(1, 1)] [TestCase(1, 2)] [TestCase(1, 3)] [TestCase(1, 4)] [TestCase(1, 5)] [TestCase(1, 6)] [TestCase(1, 7)]
        [TestCase(2, 0)] [TestCase(2, 1)] [TestCase(2, 2)] [TestCase(2, 3)] [TestCase(2, 4)] [TestCase(2, 5)] [TestCase(2, 6)] [TestCase(2, 7)]
        [TestCase(3, 0)] [TestCase(3, 1)] [TestCase(3, 2)] [TestCase(3, 3)] [TestCase(3, 4)] [TestCase(3, 5)] [TestCase(3, 6)] [TestCase(3, 7)]
        [TestCase(4, 0)] [TestCase(4, 1)] [TestCase(4, 2)] [TestCase(4, 3)] [TestCase(4, 4)] [TestCase(4, 5)] [TestCase(4, 6)] [TestCase(4, 7)]
        [TestCase(5, 0)] [TestCase(5, 1)] [TestCase(5, 2)] [TestCase(5, 3)] [TestCase(5, 4)] [TestCase(5, 5)] [TestCase(5, 6)] [TestCase(5, 7)]
        [TestCase(6, 0)] [TestCase(6, 1)] [TestCase(6, 2)] [TestCase(6, 3)] [TestCase(6, 4)] [TestCase(6, 5)] [TestCase(6, 6)] [TestCase(6, 7)]
        public void ItCorrectlyIdentifiesLegalPositions(int x, int y)
        {
            // arrange

            // act

            // assert
            Assert.IsTrue(_board.IsLegalPosition(new Point(x, y)), string.Format("({0},{1}) should be a legal position", x, y));
        }

        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(0, 8)]
        [TestCase(7, 0)]
        [TestCase(-1, 6)]
        [TestCase(6, -1)]
        [TestCase(8, 7)]
        public void ItCorrectlyIdentifiesIllegalPosition(int x, int y)
        {
            // assert
            Assert.IsFalse(_board.IsLegalPosition(new Point(x, y)), string.Format("({0},{1}) should not be a legal position", x, y));
        }

    }
}
