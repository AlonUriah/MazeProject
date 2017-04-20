using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client
{
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
        // Connection is alive or not
        private bool connected;
        // Program has exited or not
        private bool exit;
        // The client TcpClient object
        private TcpClient client;

        // The streams objects.
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;

        // The connection details
        private IPAddress ip;
        private int port;
        private int id;

        /*
         * The Client constructor.
         * It parses the ip and the port from the
         * program arguments, and sets the exit/connected
         * properties to false.
         */
        public Client(string ip, string port)
        {
            // IP Validation
            try
            {
                this.ip = IPAddress.Parse(ip);
            }
            catch (Exception e)
            {
                throw new Exception("Error: Illegal IP Address.");
            }

            // Port Validation
            try
            {
                this.port = int.Parse(port);
            }
            catch (Exception e)
            {
                throw new Exception("Error: Illegal port number.");
            }
            
            // Setting properties
            this.connected = false;
            this.exit = false;
            this.id = -1;
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
                this.client = new TcpClient();
                this.client.Connect(new IPEndPoint(this.ip, this.port));
               
            }
            catch (Exception e)
            {
                throw new Exception("Error: couldn't connect to remote host.");
            }

            // Setting the connected property to true.
            this.connected = true;

            // Sets the stream details.
            this.ns = this.client.GetStream();
            this.sr = new StreamReader(this.ns);
            this.sw = new StreamWriter(this.ns);

            // Send the unique id for authentication.
            this.sw.WriteLine(this.id);
            this.sw.Flush();

            // On startup, get a unique id from the server and save it.
            if (this.id == -1)
            {
                try
                {
                    string authentication = this.sr.ReadLine();
                    this.id = int.Parse(authentication);
                }
                catch (Exception e)
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
                while (this.connected)
                {
                    try
                    {
                        // Read a buffer from the stream
                        string response = this.sr.ReadLine();

                        // If it is null, raise an exception.
                        if (response == null)
                        {
                            //throw new Exception("Note: Connection shut down.");
                            this.connected = false;
                            continue;
                        }

                        // Otherwise, write the data to the console.
                        Console.WriteLine(">> " + response);
                    }
                    catch (Exception e)
                    {
                        this.connected = false;
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
        public void Broadcast()
        {
            // As long as the program is running...
            while (!this.exit)
            {
                try
                {
                    // Read a command from the user.
                    string broadcast = Console.ReadLine();

                    // If the command is 'exit', raise an exception.
                    if (broadcast == "exit")
                    {
                        this.connected = false;
                        this.exit = true;
                        continue;
                    }

                    // If there is no connection alive, create it.
                    if (!this.connected)
                    {
                        // Use the Connect method to reach the remote host.
                        this.Connect();
                        // Start a background task, which is listening to the server.
                        Task.Factory.StartNew(this.Listen);
                    }
                    /*
                     * After we are sure we got a connection,
                     * we write the command to the server.
                     */
                    this.sw.WriteLine(broadcast);
                    this.sw.Flush();
                }
                catch (Exception e)
                {
                    this.connected = false;
                    this.exit = true;
                }
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
            this.exit = true;
        }
    }
}
