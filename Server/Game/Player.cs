using System.Net.Sockets;

namespace Server
{
    /*
     * The Player class.
     * The class handles a player client.
     * It contains his connection data, and his unique
     * id which is determined by the server, for comparison needs.
     */
    public class Player
    {
        // Is connected property
        public bool Connected { get; set; }
        // The connection property
        public TcpClient Connection { get; set; }
        // The id property
        public int Id { get; private set; }

        /*
         * The Player constructor.
         * The method sets the properties to be
         * as received by the method's arguments.
         */
        public Player(int id, TcpClient connection)
        {
            Id = id;
            Connection = connection;
            Connected = false;
        }
    }
}
