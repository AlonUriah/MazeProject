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
    public class Client
    {
        private bool connected;
        private bool exit;
        private TcpClient client;
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;

        private IPAddress ip;
        private int port;

        public Client(string ip, string port)
        {
            try
            {
                this.ip = IPAddress.Parse(ip);
            }
            catch (Exception e)
            {
                throw new Exception("Error: Illegal IP Address.");
            }

            try
            {
                this.port = int.Parse(port);
            }
            catch (Exception e)
            {
                throw new Exception("Error: Illegal port number.");
            }

            this.connected = false;
            this.exit = false;
        }

        private void Connect()
        {
            try
            {
                this.client = new TcpClient();
                this.client.Connect(new IPEndPoint(this.ip, this.port));
            }
            catch (Exception e)
            {
                throw new Exception("Error: couldn't connect to remote host.");
            }

            this.connected = true;

            this.ns = this.client.GetStream();
            this.sr = new StreamReader(this.ns);
            this.sw = new StreamWriter(this.ns);
        }

        private void Listen()
        {
            while (this.connected)
            {
                try
                {
                    string response = this.sr.ReadLine();
                    if (response == null)
                        throw new Exception("Note: Connection shut down.");
                    
                    Console.WriteLine(">> Server: " + response);
                }

                catch (Exception e)
                {
                    this.connected = false;
                }
            }
        }

        public void Broadcast()
        {
            while (!this.exit)
            {
                try
                {
                    string broadcast = Console.ReadLine();

                    if (broadcast == "exit")
                        throw new Exception("Note: Program shutdown!");

                    if (!this.connected)
                    {
                        this.Connect();
                        Task.Factory.StartNew(this.Listen);
                    }
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

        public void Exit()
        {
            this.exit = true;
        }
    }
}
