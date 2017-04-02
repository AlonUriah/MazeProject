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
        private Model model;
        
        private List<Client> clients;
        private TcpListener listener;
        private int port;
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;
        private bool isOnline;

        private int ClientCounter;

        private readonly object clients_locker = new object();

        public Server(int port)
        {
            this.model = new Model();
            this.controller = new Controller(this.model);

            this.port = port;
            this.listener = new TcpListener(new IPEndPoint(IPAddress.Parse("localhost"), port));
            this.clients = new List<Client>();
            this.isOnline = false;

            this.ClientCounter = 0;
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
                Client client = null;
                
                lock (this.clients_locker)
                {
                    client = new Client(this.ClientCounter, currentClient);
                    this.clients.Add(client);
                    this.ClientCounter++;
                }

                Task.Factory.StartNew(() =>
                {
                    bool ClientConnected = true;
                    NetworkStream ns = currentClient.GetStream();
                    StreamReader sr = new StreamReader(ns);
                    StreamWriter sw = new StreamWriter(ns);

                    while (ClientConnected)
                    {
                        try
                        {
                            // Get a command from the client
                            string cmd = sr.ReadLine();

                            // Apply it by the controller and get the output as json
                            string json = this.controller.ApplyCommand(client.Id, cmd);

                            // Send the json's output back to the client
                            if (json != "")
                                sw.WriteLine(json);
                        }
                        catch (Exception ex)
                        {
                            ClientConnected = false;
                            sr.Close();
                            sw.Close();
                            ns.Close();

                            Console.WriteLine(ex.Message);
                            
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
