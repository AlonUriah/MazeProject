using MazeGame.Model;
using MazeGame.Model.Interfaces;
using MazeGame.ViewModel.Interfaces;

namespace MazeGame.ViewModel
{
    public class SingleplayerSettingsViewModel : ISingleplayerSettingsViewModel
    {
        private string _mazeRowsStr;
        private string _mazeColsStr; 

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
                if (int.TryParse(_mazeRowsStr, out mazeRows))
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

        public ISinglePlayerViewModel StartGame()
        {
            ISingleplayerModel singleModel = ModelFactory.Instace.GetSinglePlayerModel();
            return new SinglePlayerViewModel(singleModel, MazeName, MazeRows, MazeCols);
        }
    }
}
