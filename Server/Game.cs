using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Game
    {   
        public int[] Players { get; private set; }
        public string Name { get; private set; }

        public Game(int id, string name)
        {
            Players = new int[2];
            Players[0] = id;
            Name = name;
        }
    }
}
