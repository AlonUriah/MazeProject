using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.ModelView.GameEvents
{
    class SettingsSavedEvent : EventArgs 
    {
        public int Rows { set; get; }
        public int Cols { set; get; }
        public string GameName { set; get; }
    }
}
