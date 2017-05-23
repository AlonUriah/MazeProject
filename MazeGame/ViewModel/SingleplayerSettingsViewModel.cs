using MazeGame.Model;
using MazeGame.Model.Interfaces;
using MazeGame.ViewModel.Interfaces;

namespace MazeGame.ViewModel
{
    public class SinglePlayerSettingsViewModel : ISinglePlayerSettingsViewModel
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
            }
        }

        public int MazeRows { private set; get; }
        public int MazeCols { private set; get; }

        private bool IsValidInputs()
        {
            if (MazeCols <= 0 || MazeRows <= 0 || string.IsNullOrWhiteSpace(MazeName))
                return false;

            return true;
        }

        public ISinglePlayerViewModel StartGame()
        {
            if (!IsValidInputs()) return null;

            ISingleplayerModel singleModel = ModelFactory.Instace.GetSinglePlayerModel();
            return new SinglePlayerViewModel(singleModel, MazeName, MazeRows, MazeCols);
        }
    }
}
