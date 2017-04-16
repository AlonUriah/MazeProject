using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /*
     * The PlayCommand class.   
     * The class handles the 'play' command.
     * The command sends a player's move to his rival.
     */
    public class PlayCommand : Command
    {
        /*
         * The PlayCommand constructor.
         * Inherited by the Command class.
         */
        public PlayCommand(IModel model) : base(model)
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
            // The 'play' command gets only 1 parameter - a movement direction.
            string direction = parameters;

            // Get data by model - rival and game's name.
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
