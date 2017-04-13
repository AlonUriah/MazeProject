using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class StartCommand : Command
    {
        public StartCommand(IModel model) : base(model)
        {
        }

        public override void Execute(Player client, string parameters)
        {
            string[] args = parameters.Split(' ');
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            string name = args[0];

            Game game = this.model.AddMultiPlayerGame(client, name, rows, cols);

            if (game != null)
            {
                // Wait for another player to join the game.
                while (!game.Ready)
                    Thread.Sleep(500);

                // Send the game to the creator.
                this.Answer(client, game.ToJSON());
            }
            else
            {
                this.Answer(client, "Error: Illegal name or you're already playing.");
                client.Connection.Close();
            }
        }
    }
}
