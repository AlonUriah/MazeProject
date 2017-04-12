using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Server
{
    public class MultiPlayerGame : Game
    {
        public MultiPlayerGame(string name, Maze maze) : base(name, maze)
        {
            this.ready = false;
        }
    }
}
