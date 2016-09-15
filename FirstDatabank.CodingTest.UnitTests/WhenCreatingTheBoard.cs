namespace FirstDatabank.CodingTest.UnitTests
{
    using System.Drawing;
    using Core;
    using GamePlay;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class WhenCreatingTheBoard
    {
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

            // use the actual board - this is unit tested too
            _board = new MinefieldBoard(_squareFactory);
        }

        [Test]
        public void AllTheSquaresAreCreated()
        {
            const int rows = 5;
            const int cols = 7;
            
            // arrange
            var mockSquareFactory = new Mock<ISquareFactory>();
            mockSquareFactory.Setup(sf => sf.CreateNew(It.IsAny<float>(), It.IsAny<bool>())).Returns((float prob, bool visited) =>
            {
                var mockSquare = new Mock<IBoardSquare>();
                mockSquare.SetupGet(s => s.IsLandmine).Returns(false);
                mockSquare.SetupGet(s => s.IsVisited).Returns(visited);
                return mockSquare.Object;
            });

            // act
            var board = new MinefieldBoard(mockSquareFactory.Object);
            var startPoint = board.Initialise(rows, cols);

            // assert
            Assert.AreEqual(cols, board.BoardSize.Width);
            Assert.AreEqual(rows, board.BoardSize.Height);

            for (var row = 0; row < rows; ++row)
            {
                for (var col = 0; col < cols; ++col)
                {
                    Assert.IsTrue(board.IsLegalPosition(new Point(col, row)), "Legal position not allowed?");
                }
            }
        }

        [Test]
        public void ItInitialisesTheZeroStartingPosition()
        {
            // arrange
            const int rows = 5;
            const int cols = 7;
            var board = new MinefieldBoard(_squareFactory);

            // act
            var startPoint = board.Initialise(rows, cols);

            // assert
            Assert.AreEqual(0, startPoint.X, "Should start in left column");
            Assert.AreEqual(0, startPoint.Y, "Should start at first row");
        }
    }
}
