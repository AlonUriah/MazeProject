using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ListCommand : Command
    {
        public ListCommand(IModel model) : base(model)
        {
        }

        public override void Execute(Player client, string parameters)
        {
            List<string> list = this.model.GetAvailableGames();

            JArray lst = new JArray();

            for (int i = 0; i < list.Count; i++)
                lst.Add(list[i]);

            this.Answer(client, lst.ToString());
            client.Connection.Close();
        }
    }
}
