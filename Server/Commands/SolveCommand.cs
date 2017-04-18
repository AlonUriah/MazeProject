using MazeLib;
using Server.Adapters;
using System;
using SearchAlgorithmsLib;
using SearchAlgorithmsLib.Searchers;

namespace Server
{
    /*
     * The SolveCommand class.   
     * The class handles the 'solve' command.
     * The command ask for a solution by a BFS/DFS
     * algorithms to a specific maze.
     */
    public class SolveCommand : Command
    {
        /*
         * The SolveCommand constructor.
         * Inherited by the Command class.
         */
        public SolveCommand(IModel model) : base(model)
        {
        }
        /*
         * The Execute method.
         * The method executes a specific command,
         * according to its implementation, using
         * relevant parameters, and the sender's details
         * (Client).
         */
        public override void Execute(Player client, string parameters)
        {
            const int BFS_SYMBOL = 0;
            const int DFS_SYMBOL = 1;

            // parameters expected format: <gamename> <searchalgorithm>
            string[] args = parameters.Split(' ');            
            string name = args[0];

            Game game = model.GetGame(name);
            if (game == null)
            {
                Answer(client, "Error: No such game.");
                client.Connection.Close();
                return;
            }

            int algorithm;
            if(!int.TryParse(args[1],out algorithm) || (algorithm != BFS_SYMBOL && algorithm != DFS_SYMBOL))
            {
                Answer(client, "Error: Expected algorithm parameter is 0 or 1");
                return;
            }            

            Maze maze = game.Maze;
            Searcher<Position> searcher;
            if (algorithm == BFS_SYMBOL)
            {
                searcher = new BFS<Position>();
            }
            else
            {
                searcher = new DFS<Position>();
            }

            // Use a mazeAdpater since it implements ISearchable
            var mazeAdapter = new MazeAdapter(game.Maze);
            Solution<Position> solution = searcher.Search(mazeAdapter);

            var solutionToJasonBuilder = new JasonSolutionBuilder(solution);
            solutionToJasonBuilder["Name"] = maze.Name;
            solutionToJasonBuilder["Evaluated"] = searcher.GetNumberOfNodesEvaluated();

            // Get the solution as json.
            string response = solutionToJasonBuilder.ToJason().ToString();

            // Send the response to the client.
            this.Answer(client, response);

            // Delete the game, and close the connection.
            this.model.DeleteGame(name);
            client.Connected = false;
            client.Connection.Close();
        }
    }
}