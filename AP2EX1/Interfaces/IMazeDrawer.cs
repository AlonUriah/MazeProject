namespace AP2EX1.Interfaces
{
    /// <summary>
    /// Allowing multiple implementations in future
    /// </summary>
    public interface IMazeDrawer
    {
        /// <summary>
        /// Draw a given IMaze
        /// </summary>
        /// <param name="maze"></param>
        void DrawMaze(IMaze maze);
    }
}
