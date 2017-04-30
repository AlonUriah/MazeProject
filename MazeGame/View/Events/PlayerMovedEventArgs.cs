using System;

namespace MazeGame.View.Events
{
    public class PlayerMovedEventArgs : EventArgs //KeyPress...
    {
        public Tuple<int,int> OldLocation { get; }
        public Tuple<int,int> NewLocation { get; }
    }
}
