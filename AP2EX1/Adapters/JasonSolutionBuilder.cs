using SearchAlgorithmsLib.Interfaces;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using System.Text;
using MazeLib;

namespace AP2EX1.Adapters
{
    /// <summary>
    /// Builds a Solution Jason Object
    /// It gets a Solution in its Ctor and andd
    /// properties to an inner JObject - and return that object
    /// by calling 'ToJason()'
    /// </summary>
    public class JasonSolutionBuilder : ISolutionJasonBuilder
    {
        #region Constants and Symbols
        private const int LEFT_SYMBOL = 0;
        private const int RIGHT_SYMBOL = 1;
        private const int UP_SYMBOL = 2;
        private const int DOWN_SYMBOL = 3;
        #endregion 

        private readonly Solution<Position> _solution;
        private readonly JObject _jasonObject;

        /// <summary>
        /// Ctor.
        /// Assures JObject properties ordering by declaring them in here.
        /// </summary>
        /// <param name="solution">A solution of Positions</param>
        public JasonSolutionBuilder(Solution<Position> solution)
        {
            _solution = solution;

            // To assure properties ordering
            _jasonObject = new JObject();
            _jasonObject["Name"] = string.Empty;
            _jasonObject["Solution"] = GetSolutionStr();
            _jasonObject["NodesEvaluated"] = string.Empty;
        }

        /// <summary>
        /// Builds a string representation of this builder solution. 
        /// Specifically, string representation will hold a series of 
        /// directions to get from init Position to goal Position.
        /// For direction representaion I have used constants.
        /// </summary>
        /// <returns></returns>
        private string GetSolutionStr()
        {
            var solutionBuilder = new StringBuilder();
            // Init Position
            State<Position> currentState = _solution.States.Pop();

            State<Position> nextState;
            Direction direction;
            while (_solution.States.Count >= 1)
            {
                nextState = _solution.States.Pop();
                // An extension method for Position to get a relative Direction from Position to Position
                direction = currentState.Value.Subtract(nextState.Value);
                solutionBuilder.Append(GetDirectionSymbol(direction));

                // Expected format: 4,3,2,4,2,2 (no comma after last move)
                if (_solution.States.Count != 0)
                    solutionBuilder.Append(",");

                currentState = nextState;
            }

            return solutionBuilder.ToString();
        }

        /// <summary>
        /// Get a string representation for a given Direction.
        /// Using constants defined in Constants region.
        /// </summary>
        /// <param name="direction">A direction to get a symbol for</param>
        /// <returns></returns>
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

        /// <summary>
        /// Builder implementation - 
        /// Exposing a JToken indexers to expose inner JObject to user
        /// </summary>
        /// <param name="property">Property name in JObject</param>
        /// <returns></returns>
        public JToken this[string property]
        {
            set { _jasonObject[property] = value; }
            get { return _jasonObject[property]; }
        }

        /// <summary>
        /// Builds/Returns _jasonObject.
        /// This JObject holds solution and other properties 
        /// added by user
        /// </summary>
        /// <returns></returns>
        public JObject ToJason()
        {
            return _jasonObject;
        }
    }
}