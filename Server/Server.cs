using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using System.IO;

namespace Server
{
    /*
     * The Server class.
     * The class handles the server remote connection,
     * and is handling each client which is connecting to ask
     * for a game service.
     */
    public class Server
    {
        // Model and Controller interfaces properties.
        private IController controller;
        private IModel model;

        // player's list
        private Dictionary<int, Player> clients;

        // A TcpListener object
        private TcpListener listener;

        // A listening port
        private int port;

        // While the server is online, it is true.
        private bool isOnline;

        // Id counter, to make sure the id of the players is unique.
        private int clients_id;

        // A players list locker
        private readonly object clients_locker = new object();

        /*
         * The Server constructor.
         * The method instantiate the model and controller,
         * creates the connection, and sets the isOnline and id counter,
         * to their defaults.
         */
        public Server(int port)
        {
            this.model = new Model();
            this.controller = new Controller(this.model);

            this.port = port;
            this.listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            this.clients = new Dictionary<int, Player>();
            this.isOnline = false;

            this.clients_id = 0;
        }
        /*
         * The Start method.
         * The method starts the listening to players.
         * Moreover, each player gets a special Task for
         * handling his request, and this one is passed to 
         * the controller to take care of the request.
         */
        public void Start()
        {
            try
            {
                // Start listening to clients
                this.listener.Start();
                this.isOnline = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // While the server is still online, keep handling clients.
            while (this.isOnline)
            {
                // Accept a new client.
                TcpClient currentClient = this.listener.AcceptTcpClient();
                // Get the stream data
                NetworkStream ns = currentClient.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                int id;
                Player client = null;

                try
                {
                    string authentication = sr.ReadLine();
                    id = int.Parse(authentication);
                }
                catch (Exception e)
                {
                    continue;
                }

                if (id == -1)
                {
                    lock (this.clients_locker)
                    {
                        // Send a unique id to the client.
                        sw.WriteLine(this.clients_id);
                        sw.Flush();
                        // Save the client's data to the list.
                        client = new Player(this.clients_id, currentClient);
                        this.clients.Add(this.clients_id, client);
                        this.clients_id++;
                    }
                }
                else
                {
                    // Get the appropriate client, and update its connection details.
                    lock (this.clients_locker)
                    {
                        this.clients[id].Connection = currentClient;
                        client = this.clients[id];
                    }
                }

                // Handle the client with a specific task.
                Task.Factory.StartNew(() =>
                {
                    client.Connected = true;
                    // While the client is still connceted...
                    while (client.Connected)
                    {
                        try
                        {
                            // Get a command from the client
                            string cmd = sr.ReadLine();

                            // Print the command
                            Console.WriteLine(">> Client no. " + client.Id + ": " + cmd);

                            // Apply it by the controller
                            this.controller.ApplyCommand(client, cmd);
                        }
                        catch (Exception ex)
                        {
                            // Close the connection, and the stream.
                            client.Connected = false;
                            sr.Close();
                            ns.Close();
                        }
                    }
                });
            }
        }
        /*
         * The Stop method.
         * The method stops the server.
         */
        public void Stop()
        {
            this.isOnline = false;
        }
    }
}
