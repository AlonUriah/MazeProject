using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        private bool connected;
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

        }

        public void Connect()
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


            Task.Factory.StartNew(this.Broadcast);

            this.Listen();
        }

        private void Listen()
        {
            while (this.connected)
            {
                string response = this.sr.ReadLine();
                Console.WriteLine(">> Server: " + response);
            }
        }

        private void Broadcast()
        {
            while (this.connected)
            {
                string broadcast = Console.ReadLine();
                this.sw.WriteLine(broadcast);
                this.sw.Flush();
            }
        }

        public void Disconnect()
        {
            this.connected = false;
        }
    }
}
