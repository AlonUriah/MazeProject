using MazeLib;

namespace Server
{
    /*
     * The MultiPlayerGame class.
     * The class handles a multi player game.
     */
    public class MultiPlayerGame : Game
    {
        /*
         * The MultiPlayerGame constructor.
         * Inherited by the Game abstract class.
         */
        public MultiPlayerGame(string name, Maze maze) : base(name, maze)
        {
            // Sets the ready property to false, to wait for another player.
            this.ready = false;
        }
    }
}
