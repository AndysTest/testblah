namespace FirstDatabank.CodingTest.UnitTests
{
    using System.Drawing;
    using GamePlay;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class WhenInitialisingGamePlay
    {
        [Test]
        public void TheBoardIsPrepared()
        {
            // arrange
            var mockBoard = new Mock<IMinefieldBoard>();
            var game = new MinefieldGame(mockBoard.Object);
            Size? sizeRequested;

            mockBoard.Setup(b => b.Initialise(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int rows, int cols) =>
                {
                    sizeRequested = new Size(cols, rows);
                })
                .Returns(new Point(0, 0));

            // act
            game.Initialise();

            // assert
            mockBoard.VerifyAll();
        }
    }
}
