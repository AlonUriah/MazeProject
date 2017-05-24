using System.ComponentModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MazeGame.Model.Interfaces;
using MazeGame.ViewModel.Interfaces;
using MazeGame.Model;

namespace MazeGame.ViewModel
{
    public class MultiplayerSettingsViewModel : IMultiplayerSettingsViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _mazeRowsStr;
        private string _mazeColsStr;
        private string[] _gamesList;

        private readonly IMultiplayerSettingsModel _model;

        public MultiplayerSettingsViewModel(IMultiplayerSettingsModel model)
        {
            _model = model;
            _model.OnGamesListReceived += UpdateGamesList;
            ConnectionStatus = _model.GetList();
        }
        
        public string[] GamesList
        {
            set
            {
                _gamesList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("GamesList"));
            }
            get
            {
                return _gamesList;
            }
        }
        public int ConnectionStatus { private set; get; }
        public string SelectedGame { set; get; }

        public string MazeName { set; get; }
        public string MazeRowsStr
        {
            get
            {
                return _mazeRowsStr;
            }
            set
            {
                _mazeRowsStr = value;
                int mazeRows;
                if(int.TryParse(_mazeRowsStr,out mazeRows))
                {
                    MazeRows = mazeRows;
                    return;
                }

                //Alert
            }
        }
        public string MazeColsStr
        {
            get
            {
                return _mazeColsStr;
            }
            set
            {
                _mazeColsStr = value;
                int mazeCols;
                if (int.TryParse(_mazeColsStr, out mazeCols))
                {
                    MazeCols = mazeCols;
                    return;
                }

                //Alert
            }
        }

        public int MazeRows { private set; get; }
        public int MazeCols { private set; get; }

        private void UpdateGamesList(string gamesJArray)
        {
            JArray jArray;

            try
            {
                jArray = JArray.Parse(gamesJArray);
            }
            catch (JsonReaderException)
            {
                return;
            }

            int index = 0;
            string[] gamesList = new string[jArray.Count];
            foreach (var token in jArray)
            {
                gamesList[index] = token.Value<string>();
            }
            ConnectionStatus = 1;
        }
        
        private bool IsValidInput()
        {
            if (MazeRows <= 0 || MazeCols <= 0 || string.IsNullOrWhiteSpace(MazeName))
                return false;

            return true;
        }
        public IMultiPlayerViewModel JoinGame()
        {
            IMultiplayerModel model = ModelFactory.Instace.GetMultiPlayerModel();
            return new MultiplayerViewModel(model, SelectedGame);
        }
        public IMultiPlayerViewModel StartGame()
        {
            if (!IsValidInput()) return null; 

            IMultiplayerModel model = ModelFactory.Instace.GetMultiPlayerModel();
            return new MultiplayerViewModel(model, MazeName, MazeRows, MazeCols);
        }
    }
}
