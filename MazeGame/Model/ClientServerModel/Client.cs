using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MazeGame.Model.ClientServerModel
{
    public delegate void ResponseReceivedHandler(string response);
    /*
     * The Client class describes a client player,
     * which connects to a remote host and plays
     * the maze game. It has 7 available commands,
     * which are: generate, list, solve, close - in these commands
     * you create a connection, broadcast, get the response and shutdown
     * the connection.
     * The 3 more commands are: start, join, play - which after them
     * the connection is still alive.
     */
    public class Client
    {
        private const int SUCCESS_CODE = 1;
        private const int FAIL_CODE = 0;
        private const int EXIT_CODE = 2;

        // Connection is alive or not
        private bool _connected;
        // Program has exited or not
        private bool _exit;
        // The client TcpClient object
        private TcpClient _tcpClient;

        // The streams objects.
        private NetworkStream _networkStream;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;

        // The connection details
        private IPAddress _ip;
        private int _port;
        private int _id;

        public event ResponseReceivedHandler OnResponseReceived;

        /*
         * The Client constructor.
         * It parses the ip and the port from the
         * program arguments, and sets the exit/connected
         * properties to false.
         */
        public Client(string ip, string port)
        {
            // IP Validation
            if(!IPAddress.TryParse(ip,out _ip))
            {
                throw new Exception("Error: Illegal IP Address.");
            }

            // Port Validation
            if (!int.TryParse(port,out _port))
            {
                throw new Exception("Error: Illegal port number.");
            }
            
            // Setting properties
            _connected = false;
            _exit = false;
            _id = -1;
        }

        /*
         * The Connect method.
         * The method creates a connection to a remote
         * host by the details from the program arguments.
         * After then, it sets the connected property to true,
         * and sets the stream details.
         */
        private void Connect()
        {
            // Creating the connection
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(new IPEndPoint(_ip, _port));
               
            }
            catch (Exception e)
            {
                throw new Exception("Error: couldn't connect to remote host.",e);
            }

            // Setting the connected property to true.
            _connected = true;

            // Sets the stream details.
            _networkStream = _tcpClient.GetStream();
            _streamReader = new StreamReader(_networkStream);
            _streamWriter = new StreamWriter(_networkStream);

            // Send the unique id for authentication.
            _streamWriter.WriteLine(_id);
            _streamWriter.Flush();

            // On startup, get a unique id from the server and save it.
            if (_id == -1)
            {
                string authentication = _streamReader.ReadLine();
                if (!int.TryParse(authentication, out _id))
                {
                    throw new Exception("Error: couldn't authenticate");
                }
            }            
        }

        /*
         * The Listen method.
         * The method works as long as the connection
         * to the remote host is alive. It gets a buffer
         * from the stream, and if it's not null - it writes
         * it to the console (When we will have a GUI, it will
         * be shown as a responsive design).
         */
        private void Listen()
        {
            try
            {
                // As long as the connection is alive...
                while (_connected)
                {
                    try
                    {
                        // Read a buffer from the stream
                        string response = _streamReader.ReadLine();

                        // If it is null, raise an exception.
                        if (response == null)
                        {
                            //throw new Exception("Note: Connection shut down.");
                            _connected = false;
                         //   continue;
                        }

                        // Otherwise, write the data to the console.
                        OnResponseReceived?.Invoke(response);
                    }
                    catch (Exception)
                    {
                        _connected = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        /*
         * The Broadcast method.
         * The method works as long as the program is running.
         * It reads a command from the user, and send it to 
         * the remote host, if there is one. Otherwise, it connects
         * to the remote host and then send the command.
         */
        public int Broadcast(string query)
        {
            try
            {
                // If the command is 'exit', raise an exception.
                if (query.ToLower().Equals("exit"))
                {
                    _connected = false;
                    _exit = true;
                    return EXIT_CODE;
                }

                // If there is no connection alive, create it.
                if (!_connected)
                {
                    // Use the Connect method to reach the remote host.
                    Connect();
                    // Start a background task, which is listening to the server.
                    Task.Factory.StartNew(Listen);
                }
                /*
                 * After we are sure we got a connection,
                 * we write the command to the server.
                 */
                _streamWriter.WriteLine(query);
                _streamWriter.Flush();
                return SUCCESS_CODE;
            }
            catch (Exception)
            {
                _connected = false;
                _exit = true;
                return FAIL_CODE;
            }
        }

        /*
         * The Exit method.
         * The method exits the program, just a
         * convenience method for the future, in case
         * we would like to implement the exit button.
         */
        public void Exit()
        {
            _exit = true;
        }
    }
}
