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
        protected Model model;

        public Command(Model model)
        {
            this.model = model;
        }

        protected void Answer(Player client, string response)
        {
            using (NetworkStream ns = client.Connection.GetStream())
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.Write(response);
                sw.Flush();
            }
        }
        public abstract void Execute(Player client, string parameters);
    }
}
