using SearchAlgorithmsLib.Interfaces;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using System.Text;
using MazeLib;

namespace Server.Adapters
{
    public class JasonSolutionBuilder : ISolutionJasonBuilder
    {
        private const int LEFT_SYMBOL = 0;
        private const int RIGHT_SYMBOL = 1;
        private const int UP_SYMBOL = 2;
        private const int DOWN_SYMBOL = 3;

        private readonly Solution<Position> _solution;
        private readonly JObject _jasonObject;

        public JasonSolutionBuilder(Solution<Position> solution)
        {
            _solution = solution;

            // To assure properties ordering
            _jasonObject = new JObject();
            _jasonObject["Name"] = string.Empty;
            _jasonObject["Solution"] = GetSolutionStr();
            _jasonObject["NodesEvaluated"] = string.Empty;
        }

        private string GetSolutionStr()
        {
            var solutionBuilder = new StringBuilder();
            State<Position> currentState = _solution.States.Pop();

            State<Position> nextState;
            Direction direction;

            while (_solution.States.Count >= 1)
            {
                nextState = _solution.States.Pop();
                direction = currentState.Value.Subtract(nextState.Value);
                solutionBuilder.Append(GetDirectionSymbol(direction));

                if (_solution.States.Count != 0)
                    solutionBuilder.Append(",");

                currentState = nextState;
            }

            return solutionBuilder.ToString();
        }

        private string GetDirectionSymbol(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return UP_SYMBOL.ToString();
                case Direction.Down:
                    return DOWN_SYMBOL.ToString();
                case Direction.Left:
                    return LEFT_SYMBOL.ToString();
                default:
                    return RIGHT_SYMBOL.ToString();
            }
        }

        public JToken this[string property]
        {
            set { _jasonObject[property] = value; }
            get { return _jasonObject[property]; }
        }

        public JObject ToJason()
        {
            return _jasonObject;
        }
    }

    internal static class PositionExtensions
    {
        public static Direction Subtract(this Position from, Position to)
        {
            int fromX = from.Col, fromY = from.Row;
            int toX = to.Col, toY = to.Row;

            if (toX - fromX == -1) return Direction.Left;
            else if (toX - fromX == 1) return Direction.Right;
            else if (toY - fromY == -1) return Direction.Down;
            else return Direction.Up;
        }
    }
}