using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class JoinCommand : Command
    {
        public JoinCommand(Model model) : base(model)
        {
        }

        public override void Execute(Player client, string parameters)
        {
            string name = parameters;

            Game game = this.model.JoinMultiPlayerGame(client, name);

            if (game != null)
                this.Answer(client, game.ToJSON());
            else
                this.Answer(client, "Error: Illegal game or you're already playing.");
        }
    }
}
