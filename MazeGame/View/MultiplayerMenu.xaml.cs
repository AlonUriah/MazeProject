using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MazeGame.View
{
    /// <summary>
    /// Interaction logic for MultiplayerMenu.xaml
    /// </summary>
    public partial class MultiplayerMenu : Window
    {
        private Window parent;

        // private IViewModel vm;

        public string GameName { get; set; } = "Maor";
        public int Rows { get; set; } = 0;
        public int Cols { get; set; } = 0;

        public MultiplayerMenu(Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.DataContext = this;
        }

        private void wdw_multimenu_Close(object sender, EventArgs e)
        {
            this.parent.Show();
        }
    }
}
