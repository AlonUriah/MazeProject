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
    public class Server
    {
        private IController controller;
        private IModel model;
        
        private List<Player> clients;
        private TcpListener listener;
        private int port;

        private bool isOnline;

        private int clients_id;

        private readonly object clients_locker = new object();

        public Server(int port)
        {
            this.model = new Model();
            this.controller = new Controller(this.model);

            this.port = port;
            this.listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            this.clients = new List<Player>();
            this.isOnline = false;

            this.clients_id = 0;
        }
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
                TcpClient currentClient = this.listener.AcceptTcpClient();
                Player client = null;
                
                lock (this.clients_locker)
                {
                    client = new Player(this.clients_id, currentClient);
                    this.clients.Add(client);
                    this.clients_id++;
                }

                Task.Factory.StartNew(() =>
                {
                    bool ClientConnected = true;
                    NetworkStream ns = currentClient.GetStream();
                    StreamReader sr = new StreamReader(ns);

                    while (ClientConnected)
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
                            ClientConnected = false;
                            sr.Close();
                            ns.Close();
                            
                            lock (this.clients_locker)
                            {
                                this.clients.Remove(client);
                            }
                        }
                    }
                });
            }
        }
        public void Stop()
        {
            this.isOnline = false;
        }
        
    }
}
