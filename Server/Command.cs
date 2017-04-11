using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public abstract class Command : ICommand
    {
        protected IModel model;

        public Command(IModel model)
        {
            this.model = model;
        }

        protected void Answer(Player client, string response)
        {
            NetworkStream ns = client.Connection.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(response);
            sw.Flush();
        }
        public abstract void Execute(Player client, string parameters);
    }
}
