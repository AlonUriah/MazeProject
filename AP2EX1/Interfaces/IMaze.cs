using MazeLib;
namespace AP2EX1.Interfaces
{
    /// <summary>
    /// Increases usability for maze adapters.
    /// Implementing this interface one can treat classes 
    /// as if they were maze (as given in mazalib).
    /// </summary>
    public interface IMaze
    {
        /// <summary>
        /// Gets maze columns number
        /// </summary>
        int Cols { get; }
        /// <summary>
        /// Gets maze rows number
        /// </summary>
        int Rows { get; }
        /// <summary>
        /// Gets GoalPosition for this maze
        /// </summary>
        Position GoalPos { set; get; }
        /// <summary>
        /// Get InitialPosition for this maze
        /// </summary>
        Position InitialPos { set; get; }
        /// <summary>
        /// Return CellType for a specific Cell given 
        /// by row and col
        /// </summary>
        /// <param name="row">Cell row</param>
        /// <param name="col">Cell column</param>
        /// <returns>A CellType - Free/Wall according to location</returns>
        CellType this[int row,int col]{set;get;}
    }
}
