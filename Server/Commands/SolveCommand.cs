using MazeLib;
using Server.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
            // Split the arguments by space character.
            string[] args = parameters.Split(' ');
            // First argument - game's name.
            string name = args[0];
            // Second argument - 0 for BFS, 1 for DFS (algorithm).
            int algorithm;

            // Try to parse the algorithm number.
            try
            {
                algorithm = int.Parse(args[1]);
                if (algorithm != 0 || algorithm != 1)
                    throw new Exception("Error: No such algorithm.");
            }
            catch (Exception e)
            {
                this.Answer(client, e.Message);
                return;
            }

            // Get the game to solve.
            Game game = this.model.GetGame(name);

            // If there's such a game... solve it!
            if (game != null)
            {
                // Get the maze to solve
                Maze maze = game.Maze;

                // Solve the maze!        [---------- TO DO ----------]
                var mazeAdapter = new MazeAdapter(game.Maze);
                var bfsSearcher = new BFS();
                Solution solution = bfsSearcher.Search(mazeAdapter);
                var solutionToJasonBuilder = new SolutionJasonBuilder(solution);
                solutionToJasonBuilder["Name"] = maze.Name;
                solutionToJasonBuilder["Evaluated"] = bfsSearcher.GetEvaluated();
                //                        [---------- TO DO ----------]

                // Get the solution as json.
                string response = solutionToJasonBuilder.ToJason().ToString();

                // Send the response to the client.
                this.Answer(client, response);

                // Delete the game, and close the connection.
                this.model.DeleteGame(name);
                client.Connection.Close();
            }
            else
            {
                this.Answer(client, "Error: No such game.");
                client.Connection.Close();
            }
            


        }
    }
}
