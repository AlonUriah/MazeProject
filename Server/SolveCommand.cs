using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /*
     * The SolveCommand class.   
     * The class handles the 'solve' command.
     * The command ask for a solution by a BFS/DFS
     * algorithms to a specific maze.
     */
    public class SolveCommand : Command
    {
        /*
         * The SolveCommand constructor.
         * Inherited by the Command class.
         */
        public SolveCommand(IModel model) : base(model)
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
            throw new NotImplementedException();
        }
    }
}
