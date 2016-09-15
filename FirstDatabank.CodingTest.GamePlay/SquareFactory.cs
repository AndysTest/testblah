namespace FirstDatabank.CodingTest.GamePlay
{
    using Core;

    public interface ISquareFactory
    {
        IBoardSquare CreateNew(float squareContainsMineProbability, bool isVisited);
    }

    public class SquareFactory : ISquareFactory
    {
        #region Constants


        #endregion // constants

        #region Members

        private readonly IRandomGenerator _generator;

        #endregion // members

        #region Properties

        #endregion // properties

        #region Construction

        public SquareFactory(IRandomGenerator generator)
        {
            _generator = generator;
        }

        #endregion // construction

        #region Exposure

        public IBoardSquare CreateNew(float squareContainsMineProbability, bool isVisited)
        {
            return new BoardSquare(_generator.GenerateBool(squareContainsMineProbability), isVisited);
        }

        #endregion // exposure

        #region Helpers

        #endregion // helpers
    }
}
