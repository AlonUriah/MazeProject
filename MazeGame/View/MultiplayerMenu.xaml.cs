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
            this.game_properties.DataContext = this.multi_vm;
            this.games_list.btn_refresh.Click += this.btn_refresh_Click;
            this.games_list.btn_join.Click += this.btn_join_Click;
            this.game_properties.btn_startgame.Click += this.btn_startgame_Click;
        }

        private void btn_join_Click(object sender, RoutedEventArgs e)
        {
            IMultiPlayerViewModel vm = this.multi_vm.JoinGame();

            //while(vm.Status == 0) { }

            if(vm == null || vm.Status == 2)
            {
                MessageBox.Show("Could not join game!");
                return;
            }

            Window next = new MultiPlayerGame(vm, this.parent);
            next.Show();
            this.parent = null;
            this.Close();
        }

        private void btn_startgame_Click(object sender, RoutedEventArgs e)
        {
            this.multi_vm.MazeName = this.game_properties.txt_gamename.Text;
            this.multi_vm.MazeRowsStr = this.game_properties.txt_rows.Text;
            this.multi_vm.MazeColsStr = this.game_properties.txt_cols.Text;

            IMultiPlayerViewModel vm = this.multi_vm.StartGame();
            
            if (vm == null)
            {
                MessageBox.Show("Invalid input!");
                return;
            }

            Window next = new MultiPlayerGame(vm, this.parent);
            next.Show();
            this.parent = null;
            this.Close();
        }

        private void wdw_multimenu_Close(object sender, EventArgs e)
        {
            if (this.parent != null)
                this.parent.Show();
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            this.multi_vm.RefreshGamesList();
        }
    }
}
