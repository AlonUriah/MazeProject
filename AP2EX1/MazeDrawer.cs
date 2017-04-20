using System;
using MazeLib;
using AP2EX1.Interfaces;
using SearchAlgorithmsLib;

namespace AP2EX1
{
    /// <summary>
    /// MazeDrawer is a default implementation for IMazeDrawer.
    /// It uses symbols as defined in Constants region, by default.
    /// And yet, allows user to modify settings by reaching its Properties.
    /// </summary>
    class MazeDrawer : IMazeDrawer
    {
        #region Constants and Symbols
        private char _wallSymbol = '*';
        private char _startSymbol = '@';
        private char _endSymbol = '$';
        private char _freeSymbol = ' ';
        #endregion

        /// <summary>
        /// Wall Symbol, * by default
        /// </summary>
        public char WallSymbol
        {
            set { _wallSymbol = value; }
            get { return _wallSymbol; }
        }

        /// <summary>
        /// Start Symbol, S by default
        /// </summary>
        public char StartSymbol
        {
            set { _startSymbol = value; }
            get { return _startSymbol; }
        }

        /// <summary>
        /// End Symbol, E by default
        /// </summary>
        public char EndSymbol
        {
            set { _endSymbol = value; }
            get { return _endSymbol; }
        }

        /// <summary>
        /// Free Symbol, ' ' by default
        /// </summary>
        public char FreeSymbol
        {
            set { _freeSymbol = value; }
            get { return _freeSymbol; }
        }

        /// <summary>
        /// Draws a given IMaze
        /// </summary>
        /// <param name="maze">A maze to draw</param>
        public void DrawMaze(IMaze maze)
        {
            int height = maze.Rows;
            int width = maze.Cols;
            Position init = maze.InitialPos;
            Position goal = maze.GoalPos;            
            CellType currentCell;
            var currentPosition = new Position();

            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    currentCell = maze[i, j];
                    currentPosition.Row = i;
                    currentPosition.Col = j;

                    if (currentCell == CellType.Free && !currentPosition.Equals(init) && !(currentPosition.Equals(goal)))
                    {
                        Console.Write("{0}",FreeSymbol);
                    }
                    else if (currentCell == CellType.Free)
                    {
                        Console.Write("{0}", currentPosition.Equals(init) ? StartSymbol : EndSymbol);
                    }
                    else
                    {
                        Console.Write("{0}",WallSymbol);
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Draws a given IMaze with its solution
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="solution"></param>
        public void DrawMaze(IMaze maze, Solution<Position> solution)
        {
            int height = maze.Rows;
            int width = maze.Cols;
            Position init = maze.InitialPos;
            Position goal = maze.GoalPos;
            CellType currentCell;
            var currentPosition = new Position();

            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    currentCell = maze[i, j];
                    currentPosition.Row = i;
                    currentPosition.Col = j;

                    if (currentCell == CellType.Free)
                    {
                        //Part of solution
                        if (solution!=null && solution.Contains(currentPosition))
                        {
                            if (currentPosition.Equals(init))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(" S ");
                            }
                            else if (currentPosition.Equals(goal))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(" E ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" ^ ");
                            }
                        }
                        else
                        {
                            Console.Write("   "); //None
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" {0} ", WallSymbol);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
