namespace FirstDatabank.CodingTest.GamePlay
{
    using System.Drawing;

    public interface IMinefieldGame
    {
        void Initialise();
        bool CanMove(MoveDirection direction);
        MoveResult Move(MoveDirection direction);
        bool IsCurrentPositionAt(Point position);

        IMinefieldBoard Board { get; }
        int LivesRemaining { get; }
        bool IsFinished { get; }
    }

    public class MinefieldGame : IMinefieldGame
    {
        #region Constants

        private const int ROWS = 8;
        private const int COLUMNS = 8;
        private const int INITIAL_LIVES = 2;

        #endregion // constants

        #region Members

        #endregion // members

        #region Properties

        public IMinefieldBoard Board { get; private set; }

        public Point CurrentPosition { get; private set; }

        public int LivesRemaining { get; private set; }

        public bool IsFinished { get; private set; }

        #endregion // properties

        #region Construction

        public MinefieldGame(IMinefieldBoard board)
        {
            Board = board;
            CurrentPosition = new Point(0, 0);
        }

        #endregion // construction

        #region Exposure

        public void Initialise()
        {
            CurrentPosition = Board.Initialise(ROWS, COLUMNS);
            LivesRemaining = INITIAL_LIVES;
        }

        public bool CanMove(MoveDirection direction)
        {
            var nextPosition = GetNextPosition(direction);
            return Board.IsLegalPosition(nextPosition);
        }

        public MoveResult Move(MoveDirection direction)
        {
            var nextPosition = GetNextPosition(direction);
            var result = MoveResult.Undefined;

            if (!Board.IsLegalPosition(nextPosition))
            {
                throw new GamePlayException(string.Format("Invalid move {0}, {1}", CurrentPosition, direction));
            }

            if (Board.IsVisited(nextPosition))
            {
                result = MoveResult.Safe;
            }
            else if (Board.IsMineAt(nextPosition))
            {
                result = --LivesRemaining == 0 ? MoveResult.Dead : MoveResult.Hit;
            }

            if ((result != MoveResult.Dead) && (nextPosition.Y == ROWS - 1))
            {
                result = MoveResult.Win;
            } 
            else if (result == MoveResult.Undefined)
            {
                result = MoveResult.Safe;
            }

            CurrentPosition = nextPosition;
            IsFinished = (result == MoveResult.Dead) || (result == MoveResult.Win);
            Board.SetVisited(nextPosition);

            return result;
        }

        public bool IsCurrentPositionAt(Point position)
        {
            return CurrentPosition.Equals(position);
        }

        #endregion // exposure

        #region Helpers

        private Point GetNextPosition(MoveDirection direction)
        {
            Point nextPosition;
            switch (direction)
            {
                case MoveDirection.Down:
                    nextPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
                    break;

                case MoveDirection.Left:
                    nextPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
                    break;

                case MoveDirection.Right:
                    nextPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
                    break;

                case MoveDirection.Up:
                    nextPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
                    break;

                default:
                    throw new GamePlayException(string.Format("Unexpected move direction {0}", direction));
            }

            return nextPosition;
        }

        #endregion // helpers
    }
}
