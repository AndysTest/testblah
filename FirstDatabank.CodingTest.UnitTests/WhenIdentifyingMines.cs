namespace FirstDatabank.CodingTest.UnitTests
{
    using System.Drawing;
    using Core;
    using GamePlay;
    using Moq;
    using NUnit.Framework;

    public class WhenIdentifyingMines
    {
        private const int BOARD_ROWS = 8;
        private const int BOARD_COLUMNS = 7;

        private Mock<IRandomGenerator> _mockRandom;
        private ISquareFactory _squareFactory;
        private IMinefieldBoard _board;

        [TestFixtureSetUp]
        public void CreateBoardDependencies()
        {
            // always return false when generating bools - this makes every position contain a mine
            _mockRandom = new Mock<IRandomGenerator>();
            _mockRandom.Setup(r => r.GenerateBool(It.IsAny<float>())).Returns(true);

            // use the actual square factory - this is unit tested so we can trust it
            _squareFactory = new SquareFactory(_mockRandom.Object);

            // use the actual board - we are unit testing this
            _board = new MinefieldBoard(_squareFactory);
            _board.Initialise(BOARD_ROWS, BOARD_COLUMNS);
        }

        [Test]
        public void ItCorrectlyIdentifiesMines()
        {
            for (var row = 0; row < BOARD_ROWS; ++row)
            {
                for (var col = 0; col < BOARD_COLUMNS; ++col)
                {
                    Assert.IsTrue(_board.IsMineAt(new Point(col, row)), string.Format("There should be a mine at position ({0},{1})", col, row));
                }
            }
        }
    }
}
