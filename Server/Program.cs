using MazeGeneratorLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            if (int.TryParse(ConfigurationManager.AppSettings["port"], out port))
            {
                Server server = new Server(port);
                server.Start();
            }
        }
    }
}
