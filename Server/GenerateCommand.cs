using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class GenerateCommand : Command
    {
        public GenerateCommand(IModel model) : base(model)
        {

        }
        public override void Execute(Player client, string parameters)
        {
            string[] args = parameters.Split(' ');
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            string name = args[0];

            Game game = this.model.AddSinglePlayerGame(name, rows, cols);

            if (game != null)
                this.Answer(client, game.ToJSON());
            else
                this.Answer(client, "Error: Illegal name or you're already playing.");

            client.Connection.Close();
        }
    }
}
