namespace FirstDatabank.CodingTest
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using GamePlay;
    using Hci;

    public interface IGameEngine
    {
        void PlayGame(IMinefieldGame game);
    }

    public class GameEngine : IGameEngine
    {
        private readonly IInputSource _input;
        private readonly IOutputTarget _output;

        private IMinefieldGame _game;

        public GameEngine(IInputSource input, IOutputTarget output)
        {
            _input = input;
            _output = output;
        }

        public void PlayGame(IMinefieldGame game)
        {
            _game = game;
            _game.Initialise();

            while (!game.IsFinished)
            {
                DumpBoard();
                MakeNextMove();
            }

            GameOver();
        }

        private void GameOver()
        {
            _output.WriteLine("Game over.");
            DumpBoard();
        }

        private void DumpBoard()
        {
            var rows = _game.Board.GetBoardSquares();
            var boardRow = rows as IList<IEnumerable<IBoardSquare>> ?? rows.ToList();

            for (var rowIndex = boardRow.Count - 1; rowIndex >= 0; rowIndex--)
            {
                var row = boardRow[rowIndex];
                var rowList = row as IList<IBoardSquare> ?? row.ToList();
                var sep = GenerateSeparatorRow(boardRow.Count);

                _output.WriteLine(sep);
                _output.WriteLine(GenerateRow(rowList, rowIndex));
                if (rowIndex == 0)
                {
                    _output.WriteLine(sep);
                }

            }

        }

        private static string GenerateSeparatorRow(int width)
        {
            var sb = new StringBuilder();

            for (var idx = 0; idx < width; ++idx)
            {
                if (idx == 0)
                {
                    sb.Append(" ");
                }
                sb.Append("- ");
            }

            return sb.ToString();
        }

        private string GenerateRow(IList<IBoardSquare> rowSquares, int rowIndex)
        {
            var sb = new StringBuilder();

            for (var idx = 0; idx < rowSquares.Count; ++idx)
            {
                var square = rowSquares[idx];

                if (idx == 0)
                {
                    sb.Append("|");
                }

                sb.Append(GetRepresentationOf(square, new Point(idx, rowIndex)));
                sb.Append("|");
            }

            return sb.ToString();
        }

        private char GetRepresentationOf(IBoardSquare square, Point currentPosition)
        {
            if (_game.IsCurrentPositionAt(currentPosition))
                return '.';

            if (!square.IsVisited)
                return '?';

            return square.IsLandmine ? '*' : ' ';
        }

        private void MakeNextMove()
        {
            var direction = GetNextMoveDirection();
            
            if (_game.CanMove(direction))
            {
                var result = _game.Move(direction);
                switch (result)
                {
                    case MoveResult.Dead:
                        _output.WriteLine("You died.");
                        break;

                    case MoveResult.Hit:
                        _output.WriteLine(string.Format("You hit a mine. You have {0} {1} remaining.",
                            _game.LivesRemaining, _game.LivesRemaining == 1 ? "life" : "lives"));
                        break;

                    case MoveResult.Safe:
                        _output.WriteLine("You moved.");
                        break;

                    case MoveResult.Win:
                        _output.WriteLine("Congratulations - you won!");
                        break;

                    default:
                        _output.WriteLine(
                            "Something unfortunate happended to my inner joojoo which makes me feel woozy.  Have another go and we'll see how it pans out.");
                        break;
                }
            }
            else
            {
                _output.WriteLine(string.Format("Illegal move."));
            }
        }

        private MoveDirection GetNextMoveDirection()
        {
            MoveDirection? direction = null;
            while (!direction.HasValue)
            {
                _output.Write("Enter direction: U(p), D(own) L(eft), R(ight): ");
                // get an input
                switch (_input.GetKeyStroke())
                {
                    case 'U':
                    case 'u':
                        direction = MoveDirection.Up;
                        break;

                    case 'D':
                    case 'd':
                        direction = MoveDirection.Down;
                        break;

                    case 'L':
                    case 'l':
                        direction = MoveDirection.Left;
                        break;

                    case 'R':
                    case 'r':
                        direction = MoveDirection.Right;
                        break;
                }
            }
            _output.WriteLine(null);

            return direction.Value;
        }
    }
}
