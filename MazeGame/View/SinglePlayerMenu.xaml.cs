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
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        private Window parent;
        private ISinglePlayerSettingsViewModel single_vm;

        public SinglePlayerMenu(Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.single_vm = new SinglePlayerSettingsViewModel();
            this.game_properties.btn_startgame.Click += this.btn_startgame_Click;
        }

        private void btn_startgame_Click(object sender, RoutedEventArgs e)
        {
            this.single_vm.MazeName = this.game_properties.txt_gamename.Text;
            this.single_vm.MazeRowsStr = this.game_properties.txt_rows.Text;
            this.single_vm.MazeColsStr = this.game_properties.txt_cols.Text;


            ISinglePlayerViewModel vm = this.single_vm.StartGame();



            if (vm == null)
            {
                MessageBox.Show("Invalid input!");
                return;
            }

            Window next = new SinglePlayerGame(vm, this.parent);
            next.Show();
            this.parent = null;
            this.Close();
        }

        private void wdw_singlemenu_Close(object sender, EventArgs e)
        {
            if (this.parent != null)
                this.parent.Show();
        }
    }
}
    