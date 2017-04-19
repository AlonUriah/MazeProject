using MazeLib;

namespace Server.Interfaces
{
    public interface IMaze
    {
        int Cols { get; }
        int Rows { get; }
        Position GoalPos { set; get; }
        Position InitialPos { set; get; }
        CellType this[int row,int col]{set;get;}
    }
}
