using System;
using MazeGeneratorLib;
using MazeLib;
using AP2EX1.Adapters;
using SearchAlgorithmsLib;
using SearchAlgorithmsLib.Interfaces;
using SearchAlgorithmsLib.Searchers;
using AP2EX1.Comperators;

namespace AP2EX1
{
    /// <summary>
    /// Main Class Testing 
    /// SearchLib and Drawing Maze
    /// </summary>
    class Program
    {
        /// <summary>
        /// Compares BFS and DFS solvers.
        /// </summary>
        public static void CompareSolvers(Maze maze)
        {            
            var searchableMaze = new MazeAdapter(maze);

            int dfsNodesEvaluated, bfsNodesEvaluated;
            Solution<Position> dfsSolution, bfsSolution;

            Searcher<Position> searcher = new BFS<Position>(new CostComperator<Position>());
            bfsSolution = searcher.Search(searchableMaze);
            bfsNodesEvaluated = searcher.GetNumberOfNodesEvaluated();

            searcher = new DFS<Position>();
            dfsSolution = searcher.Search(searchableMaze);
            dfsNodesEvaluated = searcher.GetNumberOfNodesEvaluated();
            
            bool isBfsBetter = dfsNodesEvaluated > bfsNodesEvaluated;

            Console.WriteLine("Better solution was found by {0} ({1} vs. {2} nodes evaluated)",
                isBfsBetter ? "BFS" : "DFS",
                isBfsBetter ? bfsNodesEvaluated : dfsNodesEvaluated,
                isBfsBetter ? dfsNodesEvaluated : bfsNodesEvaluated);

            var drawer = new MazeDrawer();
            drawer.DrawMaze(searchableMaze,isBfsBetter ? bfsSolution:dfsSolution);
        }

        /// <summary>
        /// Main class.
        /// Generates a Maze.
        /// Sends it CompareSolvers to compare two solvers and draw it.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(30, 30);

            CompareSolvers(maze);            

            Console.Read();
        }
    }
}
