using MazeGame.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeGame.ViewModel
{
    public class VM : ISinglePlayerViewModel
    {
        private MazeGeneratorLib.DFSMazeGenerator mazer;
        private Maze maze;

        public VM()
        {
            this.mazer = new MazeGeneratorLib.DFSMazeGenerator();
            this.maze = this.mazer.Generate(10, 10);
            this.maze.Name = "Uriah";
        }

        public Maze SingleMaze
        {
            get
            {
                return this.maze;                
            }
        }
        public int PlayerRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int PlayerColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void PlayerMoved(object sender, string direction)
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void Solve()
        {
            throw new NotImplementedException();
        }
    }
}
