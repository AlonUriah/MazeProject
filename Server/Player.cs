using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Player
    {
        public TcpClient Connection { get; private set; }
        public int Id { get; private set; }

        public Player(int id, TcpClient connection)
        {
            Id = id;
            Connection = connection;
        }
    }
}
