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
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        private Window parent;

        public SinglePlayerMenu(Window parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void wdw_singlemenu_Close(object sender, EventArgs e)
        {
            this.parent.Show();
        }
    }
}
