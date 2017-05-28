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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGame.View.Controls
{
    /// <summary>
    /// Interaction logic for GameProperties.xaml
    /// </summary>
    public partial class GameProperties : UserControl
    {

        public GameProperties()
        {
            InitializeComponent();
            this.txt_cols.Text = Properties.Settings.Default["Cols"].ToString();
            this.txt_rows.Text = Properties.Settings.Default["Rows"].ToString();
        }
    }
}
