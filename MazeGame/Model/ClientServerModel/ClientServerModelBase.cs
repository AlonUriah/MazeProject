using System.Configuration;

namespace MazeGame.Model.ClientServerModel
{
    public abstract class ClientServerModelBase
    {
        protected readonly Client _client;

        public bool IsLoading { set; get; } = true;

        public ClientServerModelBase()
        {
            string ipAddressStr = ConfigurationManager.AppSettings["IpAddress"];
            string portStr = ConfigurationManager.AppSettings["PortNumber"];
            _client = new Client(ipAddressStr, portStr);
            _client.OnResponseReceived += ResponseReceived;
        }

        public ClientServerModelBase(Client client)
        {
            _client = client;
            _client.OnResponseReceived += ResponseReceived;
        }

        public abstract void ResponseReceived(string response);

        public void Close()
        {
            _client.Exit();
        }
    }
}
