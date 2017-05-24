using System.Configuration;
using MazeGame.Model.Interfaces;
using MazeGame.Model.ClientServerModel;

namespace MazeGame.Model
{
    public class ModelFactory
    {
        private static ModelFactory _instance;
        private readonly Client _client;
        private static object _locker = new object();

        private ModelFactory()
        {
            string ipAddressStr = Properties.Settings.Default.IP;
            string portStr = Properties.Settings.Default.Port;
            _client = new Client(ipAddressStr, portStr);
        }

        public static ModelFactory Instace
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new ModelFactory();
                        }
                    }
                }

                return _instance;
            }
        }

        public ISingleplayerModel GetSinglePlayerModel()
        {
            return new ClientServerSingleplayerModel(_client);
        }
        public IMultiplayerModel GetMultiPlayerModel(string gameName, int mazeRows, int mazeCols)
        {
            return new ClientServerMultiplayerModel(_client, gameName, mazeRows, mazeCols);
        }

    }
}
