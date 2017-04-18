using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /*
     * The GenerateCommand class.
     * The class handles the 'generate' command.
     * The command generates a new single player game,
     * and retrieves it to the player.
     */
    public class GenerateCommand : Command
    {
        /*
         * The GenerateCommand constructor.
         * Inherited by the Command class.
         */
        public GenerateCommand(IModel model) : base(model)
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
            // Split the arguments by the space character.
            string[] args = parameters.Split(' ');
            // First arg - the game's name.
            string name = args[0];
            // Second arg - the game's number of rows.
            int rows = int.Parse(args[1]);
            // Third arg - the game's number of cols.
            int cols = int.Parse(args[2]);
            
            // Create the game according to the arguments.
            Game game = this.model.AddSinglePlayerGame(name, rows, cols);

            /*
             * If the game is not null, means the creation succeeded,
             * send it serialized to the client who asked for it.
             * Otherwise, send an appropriate error.
             */
            if (game != null)
                this.Answer(client, game.ToJSON());
            else
                this.Answer(client, "Error: Illegal name or you're already playing.");

            // Anyways, close the connection.
            client.Connected = false;
            client.Connection.Close();
        }
    }
}
