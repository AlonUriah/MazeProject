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

        private readonly object mazes_locker = new object();
        private readonly object solutions_locker = new object();
        private readonly object games_locker = new object();

        public Model()
        {
            this.mazes = new Dictionary<string, Maze>();
            this.solutions = new Dictionary<string, Solution[]>();
            this.games = new Dictionary<string, Game>();
        }
        
        public void AddMaze(string name, int rows, int cols)
        {
            lock (this.mazes_locker)
            {
                this.mazes.Add(name, new Maze(rows, cols) { Name = name });
            }
        }

        public void AddGame(string name, string maze)
        {
            
        }
    }
}
