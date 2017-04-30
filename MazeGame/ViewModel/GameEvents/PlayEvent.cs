using System;
using System.Text;

namespace MazeGame.ModelView.GameEvents
{
    class PlayEvent : EventArgs
    {
        private string _playCommand;
        private PlayEvent pe;
        
        public PlayEvent(string playInfo)
        {
            _playCommand = string.Format("play {0}", playInfo);
            EventHandler eh;
            /*
             * PlayEventHandler (sender = this, e);
             */
        }
    }
}
