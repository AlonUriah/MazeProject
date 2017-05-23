namespace MazeGame.Common
{
    public class MazeWrapper
    {
        public string MazeStr { set; get; }
        public int Cols { set; get; }
        public int Rows { set; get; }
        public string Name { set; get; }
        public int StartRow { set; get; }
        public int StartCol { set; get; }
        public int EndRow { set; get; }
        public int EndCol { set; get; }

        private char GetCharAtPosition(int row, int col)
        {
            return MazeStr[(row * Cols) + col];
        }
        private void SetCharAtPosition(int row, int col, char c)
        {
            char[] mazeStr = MazeStr.ToCharArray();
            mazeStr[(row * col) + col] = c;
            MazeStr = mazeStr.ToString();
        }

        public char this[int row, int col]
        {
            set
            {
                SetCharAtPosition(row, col, value);
            }
            get
            {
                return GetCharAtPosition(row, col);  
            }
        }
    }
}
