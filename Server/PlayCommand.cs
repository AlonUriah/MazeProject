using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class PlayCommand : Command
    {
        public PlayCommand(IModel model) : base(model)
        {
        }

        public override void Execute(Player client, string parameters)
        {
            string direction = parameters;

            // Get data by model
            Player rival = this.model.GetRival(client);
            string game_name = this.model.GetGame(client);
            try
            {
                // Serialize the move to json
                JObject move = new JObject();
                move["Name"] = game_name;
                move["Direction"] = direction;

                // Send the answer to the rival
                this.Answer(rival, move.ToString());
            }
            catch (Exception e)
            {
                this.Answer(client, "Error: player has disconnected.");
            }
        }
    }
}
