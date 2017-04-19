using MazeLib;

namespace Server
{
    /*
     * The SinglePlayerGame class.
     * The class handles a single player game.
     */
    public class SinglePlayerGame : Game
    {
        /*
         * The SinglePlayerGame constructor.
         * Inherited by the Game abstract class.
         */
        public SinglePlayerGame(string name, Maze maze) : base(name, maze)
        {
            // Sets the ready property to true, since there's no need to wait for players.
            this.ready = true;
        }
    }
}
