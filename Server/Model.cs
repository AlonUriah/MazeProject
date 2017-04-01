using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;

namespace Server
{
    public class Model
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution[]> solutions;
        private Dictionary<string, Game> games;

        public Model()
        {
            this.mazes = new Dictionary<string, Maze>();
            this.solutions = new Dictionary<string, Solution[]>();
            this.games = new Dictionary<string, Game>();
        }
        
        
    }
}
