using MazeGame.ViewModel;
using MazeGame.ViewModel.Interfaces;
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
        private IMultiplayerSettingsViewModel multi_vm;

        public MultiplayerMenu(Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.multi_vm = new MultiplayerSettingsViewModel();
            this.games_list.DataContext = this.multi_vm;
        }

        private void wdw_multimenu_Close(object sender, EventArgs e)
        {
            this.parent.Show();
        }
    }
}
