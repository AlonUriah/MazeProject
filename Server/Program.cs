using MazeGeneratorLib;
using System;
using System.Collections.Generic;
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
            Game game = new SinglePlayerGame("Hello", new DFSMazeGenerator().Generate(5, 7));

            Console.WriteLine(game.ToJSON());


            Console.ReadKey();
        }
    }
}
