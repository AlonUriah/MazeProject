using MazeGame.Model.Interfaces;
using MazeGame.ViewModel.Interfaces;

namespace MazeGame.ViewModel
{
    public class MultiplayerSettingsViewModel : IMultiplayerSettingsViewModel
    {
        private string _mazeRowsStr;
        private string _mazeColsStr;
        private readonly IMultiplayerSettingsModel _model;

        public MultiplayerSettingsViewModel(IMultiplayerSettingsModel model)
        {
            _model = model;
            _model.OnGamesListReceived += UpdateGamesList;
            
        }


        public int ConnectionStatus { private set; get; }
        public string SelectedGame { set; get; }
        public string[] GamesList { set; get; }
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

        public void JoinGame()
        {
            _model.JoinGame(SelectedGame);
        }
        public void StartGame()
        {
            if(MazeRows == 0 || MazeCols == 0 || string.IsNullOrWhiteSpace(MazeName))
            {
                //Alert
                return;
            }

            _model.StartGame(MazeName, MazeRows, MazeCols);
        }

        private void UpdateGamesList(string gamesList)
        {
            string[] spl = gamesList.Split(',');
            GamesList = spl;
        }
    }
}
