namespace FirstDatabank.CodingTest.UnitTests
{
    using Core;
    using GamePlay;
    using Moq;
    using NUnit.Framework;

    public class WhenMovingInTheGame
    {
        private const int BOARD_ROWS = 11;
        private const int BOARD_COLUMNS = 13;

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
            _board.Initialise(BOARD_ROWS, BOARD_COLUMNS);
        }

        [Test]
        public void YouCanMoveRightOfStartingPosition()
        {
            // arrange
            var game = new MinefieldGame(_board);
            game.Initialise();

            // act
            var moveResult = game.CanMove(MoveDirection.Right);

            // assert
            Assert.IsTrue(moveResult, "Should be able to move right of starting position");
        }

        [Test]
        public void YouCanMoveUpFromStartingPosition()
        {
            // arrange
            var game = new MinefieldGame(_board);
            game.Initialise();

            // act
            var moveResult = game.CanMove(MoveDirection.Up);

            // assert
            Assert.IsTrue(moveResult, "Should be able to move up from starting position");
        }

        [Test]
        public void YouCannotMoveRightPastLastColumn()
        {
            // arrange
            var game = new MinefieldGame(_board);
            var moveResult = MoveResult.Undefined;

            game.Initialise();

            for (var col = 0; col < 7; ++col)
            {
                moveResult = game.Move(MoveDirection.Right);
            }

            // act
            var canMove = game.CanMove(MoveDirection.Right);

            // assert
            Assert.IsFalse(canMove, "Should not be able to move right past last column");
        }

        [Test]
        public void AttemptToMovePastLastColumnThrows()
        {
            // arrange
            var game = new MinefieldGame(_board);
            var moveResult = MoveResult.Undefined;

            game.Initialise();

            for (var col = 0; col < 7; ++col)
            {
                moveResult = game.Move(MoveDirection.Right);
            }

            // act/assert
            var exception = Assert.Throws<GamePlayException>(() => game.Move(MoveDirection.Right));

            // assert
            Assert.AreEqual("Invalid move {X=7,Y=0}, Right", exception.Message, "Exception message should say not a valid move");
        }

        [Test]
        public void YouCannotMoveDownFromFirstRow()
        {
            // arrange
            var game = new MinefieldGame(_board);

            game.Initialise();

            // act
            var moveResult = game.CanMove(MoveDirection.Down);

            // assert
            Assert.IsFalse(moveResult, "Should not be able to move below starting row");
        }

        [Test]
        public void AttemptToMoveDownFromFirstRowThrows()
        {
            // arrange
            var game = new MinefieldGame(_board);

            game.Initialise();

            // act
            var exception = Assert.Throws<GamePlayException>(() => game.Move(MoveDirection.Down));

            // assert
            Assert.AreEqual("Invalid move {X=0,Y=0}, Down", exception.Message, "Exception message should say not a valid move");
        }

        [Test]
        public void AttemptToMoveLeftFromFirstColumnThrows()
        {
            // arrange
            var game = new MinefieldGame(_board);

            game.Initialise();

            // act
            var exception = Assert.Throws<GamePlayException>(() => game.Move(MoveDirection.Left));

            // assert
            Assert.AreEqual("Invalid move {X=0,Y=0}, Left", exception.Message, "Exception message should say not a valid move");
        }


        [Test]
        public void YouCannotMoveUpFromTopRow()
        {
            // arrange
            var game = new MinefieldGame(_board);
            var moveResult = MoveResult.Undefined;

            game.Initialise();

            for (var col = 0; col < 7; ++col)
            {
                moveResult = game.Move(MoveDirection.Up);
            }

            // act
            var canMove = game.CanMove(MoveDirection.Up);

            // assert
            Assert.IsFalse(canMove, "Should not be able to move above top row");
        }

        [Test]
        public void YouWinWhenYouGetToTheTopRow()
        {
            // arrange
            var game = new MinefieldGame(_board);
            var moveResult = MoveResult.Undefined;

            game.Initialise();

            for (var col = 0; col < 6; ++col)
            {
                moveResult = game.Move(MoveDirection.Up);
            }

            // act
            moveResult = game.Move(MoveDirection.Up);

            // assert
            Assert.AreEqual(MoveResult.Win, moveResult, "Should win when you get to the top row");
        }
    }
}
