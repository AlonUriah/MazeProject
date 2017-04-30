namespace MazeGame.ModelView
{
    class MazeViewModel
    {
        public string GameName { set; get; }        // Data binding to single player settings view
        public int Rows { set; get; }
        public int Cols { set; get; }

        //On StartGame ---> GenerateSinglePlayerGame()
    }
}
