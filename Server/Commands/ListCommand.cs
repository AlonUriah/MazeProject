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
     * The ListCommand class.   
     * The class handles the 'list' command.
     * The command presents a list of available multi player
     * games to join.
     */
    public class ListCommand : Command
    {
        /*
         * The ListCommand constructor.
         * Inherited by the Command class.
         */
        public ListCommand(IModel model) : base(model)
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
            // Get the games' list by the model.
            List<string> list = this.model.GetAvailableGames();

            // Create a json array.
            JArray lst = new JArray();

            // Add all the games' names to the json array.
            for (int i = 0; i < list.Count; i++)
                lst.Add(list[i]);

            // Return the list, and close the connection.
            this.Answer(client, lst.ToString());
            client.Connected = false;
            client.Connection.Close();
        }
    }
}
