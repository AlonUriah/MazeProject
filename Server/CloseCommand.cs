using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class CloseCommand : Command
    {
        public CloseCommand(IModel model) : base(model)
        {
        }


        public override void Execute(Player client, string parameters)
        {
            // Get game name
            string name = this.model.GetGame(client);

            // Get the rival
            Player rival = this.model.GetRival(client);

            // Send both clients an empty json, means closed.
            this.Answer(client, "Game ended.");
            this.Answer(rival, "Game ended.");

            // Delete the game.
            this.model.DeleteMultiPlayerGame(name);
        }
    }
}
