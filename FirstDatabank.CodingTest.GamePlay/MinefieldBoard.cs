namespace FirstDatabank.CodingTest.GamePlay
{
    using System.Collections.Generic;
    using System.Drawing;

    public interface IMinefieldBoard
    {
        Point Initialise(int rows, int columns);
        bool IsMineAt(Point position);
        bool IsVisited(Point position);
        bool IsLegalPosition(Point position);
        IEnumerable<IEnumerable<IBoardSquare>> GetBoardSquares();
        void SetVisited(Point position);
    }

    public class MinefieldBoard : IMinefieldBoard
    {
        #region Constants

        private const float PROBABILITY_THAT_SQUARE_CONTAINS_MINE = 0.15f;
        private const float PROBABILITY_ALWAYS = 1.0f;
        private const float PROBABILITY_NEVER = 0.0f;

        private const int INVALID_INDEX = -1;

        #endregion // constants

        #region Members

        private readonly ISquareFactory _squareFactory;
        private readonly IList<IBoardSquare> _squares;

        #endregion // members

        #region Properties

        public Size BoardSize { get; private set; }

        #endregion // properties

        #region Construction

        public MinefieldBoard(ISquareFactory squareFactory)
        {
            _squareFactory = squareFactory;
            _squares = new List<IBoardSquare>();
        }


        #endregion // construction

        #region Exposure

        public Point Initialise(int rows, int columns)
        {
            _squares.Clear();
            BoardSize = new Size(columns, rows);
            // start position is always clear
            _squares.Add(_squareFactory.CreateNew(PROBABILITY_NEVER, true));
            for (var i = 1; i < rows * columns; ++i)
            {
                _squares.Add(_squareFactory.CreateNew(PROBABILITY_THAT_SQUARE_CONTAINS_MINE, false));
            }

            return new Point(0, 0);
        }

        public bool IsMineAt(Point position)
        {
            return IsMineAt(position.X, position.Y);
        }

        public bool IsVisited(Point position)
        {
            return IsVisited(position.X, position.Y);
        }

        public bool IsLegalPosition(Point position)
        {
            return GetIndexAt(position.X, position.Y) >= 0;
        }

        public IEnumerable<IBoardSquare> GetRow(int row)
        {
            var rowContents = new List<IBoardSquare>(BoardSize.Width);

            for (var col = 0; col < BoardSize.Width; ++col)
            {
                rowContents.Add(GetSquareAt(col, row));
            }

            return rowContents;
        }

        public IEnumerable<IEnumerable<IBoardSquare>> GetBoardSquares()
        {
            var rows = new List<IEnumerable<IBoardSquare>>(BoardSize.Height);

            for (var row = 0; row < BoardSize.Height; ++row)
            {
                rows.Add(GetRow(row));
            }

            return rows;
        }

        public void SetVisited(Point position)
        {
            var square = GetSquareAt(position.X, position.Y);
            if (square != null)
            {
                square.IsVisited = true;
            }
        }

        #endregion // exposure

        #region Helpers

        private bool IsMineAt(int x, int y)
        {
            var cell = GetSquareAt(x, y);
            return cell.IsLandmine;
        }

        private bool IsVisited(int x, int y)
        {
            var cell = GetSquareAt(x, y);
            return cell.IsVisited;
        }

        private IBoardSquare GetSquareAt(int x, int y)
        {
            var index = GetIndexAt(x, y);
            if (index != INVALID_INDEX)
            {
                return _squares[index];
            }

            throw new GamePlayException(string.Format("Not a valid board position ({0},{1})", x, y));

        }

        private int GetIndexAt(int x, int y)
        {
            var index = (y*BoardSize.Width) + x;

            return (x >= 0) && (y >= 0) && (_squares.Count > 0) && (index < _squares.Count) && (x < BoardSize.Width) && (y < BoardSize.Height)
                ? index 
                : INVALID_INDEX;
        }

        #endregion // helpers
    }
}
