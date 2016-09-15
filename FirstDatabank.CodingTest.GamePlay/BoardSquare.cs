namespace FirstDatabank.CodingTest.GamePlay
{
    public interface IBoardSquare
    {
        bool IsLandmine { get; }
        bool IsVisited { get; set; }
    }

    public class BoardSquare : IBoardSquare
    {
        #region Constants

        #endregion // constants

        #region Members

        #endregion // members

        #region Properties

        public bool IsLandmine { get; private set; }

        public bool IsVisited { get; set; }

        #endregion // properties

        #region Construction

        public BoardSquare(bool isLandmine, bool isVisited)
        {
            IsLandmine = isLandmine;
            IsVisited = isVisited;
        }

        #endregion // construction

        #region Exposure

        #endregion // exposure

        #region Helpers

        #endregion // helpers
    }
}
