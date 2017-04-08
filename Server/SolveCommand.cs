using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class SolveCommand : Command
    {
        public SolveCommand(Model model) : base(model)
        {
        }


        public override void Execute(Player client, string parameters)
        {
            throw new NotImplementedException();
        }
    }
}
