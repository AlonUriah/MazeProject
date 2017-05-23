using System.Threading;
using System.Configuration;
using MazeGame.Model.Interfaces;
using MazeLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MazeGame.Common;

namespace MazeGame.ViewModel
{
    class SinglePlayerViewModel : SinglePlayerViewModelBase
    {
        const char LEFT_CHAR = '3';
        const char RIGHT_CHAR = '2';
        const char UP_CHAR = '1';
        const char DOWN_CHAR = '0';
        const char FREE_CELL_CHAR = '0';

        private ISingleplayerModel _model;
        private MazeWrapper _maze;

        public bool IsLoading { set; get; } = true;
        
        public SinglePlayerViewModel(ISingleplayerModel model, string name, int rows, int cols)
        {
            _model = model;
            _model.OnGameReceived += UpdateMaze;
            _model.CreateNewGame(name, rows, cols);
        }

        private void UpdateMaze(object sender, string game)
        {
            JObject gameJason;
            OnPlayerMoved += SinglePlayerMoved;
            try
            {
                gameJason = JObject.Parse(game);
            }
            catch (JsonReaderException e)
            {
                //Alert
                return;
            }

            _maze = gameJason.ToMazeWrapper();
        }

        public override MazeWrapper PlayerMaze
        {
            get
            {
                return _maze;
            }

            set
            {
                _maze = value;
            }
        }

        private bool IsValidMove(string direction, out Position position)
        {
            int row = PlayerRow;
            int column = PlayerColumn;

            switch (direction.ToLower())
            {
                case "up":
                    row++;
                    break;
                case "down":
                    row--;
                    break;
                case "left":
                    column--;
                    break;
                case "right":
                    column++;
                    break;
            }

            position = new Position(row, column);
            return (_maze[row, column] == FREE_CELL_CHAR);
        }
        public void SinglePlayerMoved(object sender, string direction)
        {
            Position position;
            if (!IsValidMove(direction, out position)) return;

            PlayerColumn = position.Col;
            PlayerRow = position.Row;
        }
        public override void Solve()
        {
            var config = ConfigurationManager.AppSettings;
            _model.OnSolutionReceived += AnimateSolution;
            _model.SolveMaze(_maze.Name,config["algorithm"]);
        }
        private void AnimateSolution(object sender, string solution)
        {
            foreach(char dirChr in solution)
            {
                switch (dirChr)
                {
                    case LEFT_CHAR:
                        PlayerColumn--;
                        break;
                    case RIGHT_CHAR:
                        PlayerColumn++;
                        break;
                    case UP_CHAR:
                        PlayerRow++;
                        break;
                    case DOWN_CHAR:
                        PlayerRow--;
                        break;
                }
                Thread.Sleep(1000);
            }
        }
        public override void Restart()
        {
            PlayerRow = _maze.StartRow;
            PlayerColumn = _maze.StartCol;
        }
        public override char GetValueAtPos(Position pos)
        {
            return _maze[pos.Row, pos.Col];
        }
    }
}