namespace AP2EX1.Interfaces
{
    /// <summary>
    /// IDrawable interface.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Returns a char representation for a specific location on screen.
        /// </summary>
        /// <param name="x">X axis location</param>
        /// <param name="y">Y axis location</param>
        /// <returns>A char representation</returns>
        char Draw(int x, int y);
    }
}
