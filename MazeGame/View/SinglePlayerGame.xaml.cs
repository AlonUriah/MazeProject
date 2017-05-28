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
    /// Interaction logic for SinglePlayerGame.xaml
    /// </summary>
    public partial class SinglePlayerGame : Window
    {
        private ISinglePlayerViewModel vm;
        private Window parent;

        

        public SinglePlayerGame(ISinglePlayerViewModel vm, Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.vm = vm;
            this.player.DataContext = this.vm;
            this.KeyDown += this.GameSurface_KeyDown;
            this.vm.GameStatusChanged += this.OnStatusChanged;
        }

        private void OnStatusChanged(object sender, Nullable<int> status)
        {
            switch (status)
            {
                case 0:
                    MessageBox.Show("You win!");
                    this.Close();
                    break;
                case 1:
                    MessageBox.Show("You lost!");
                    this.Close();
                    break;
                case 2:
                    MessageBox.Show("Connection lost by one of players.");
                    break;
                default:
                    break;
            }
        }

        private void GameSurface_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    this.player.Key = "up";
                    break;
                case Key.Down:
                    this.player.Key = "down";
                    break;
                case Key.Right:
                    this.player.Key = "right";
                    break;
                case Key.Left:
                    this.player.Key = "left";
                    break;
                default:
                    break;
            }
            this.vm.PlayerMoved("player", this.player.Key);
        }

        private void btn_restart_Click(object sender, RoutedEventArgs e)
        {
            this.vm.Restart();
        }

        private void btn_solve_Click(object sender, RoutedEventArgs e)
        {
            this.KeyDown -= this.GameSurface_KeyDown;
            this.vm.Solve();
            this.btn_solve.IsEnabled = false;
            this.btn_restart.IsEnabled = false;
        }

        private void btn_quit_Click(object sender, RoutedEventArgs e)
        {
            if (this.btn_solve.IsEnabled)
                this.vm.Close();
            this.Close();
            this.parent.Show();
        }

        private void wdw_single_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.btn_solve.IsEnabled)
                this.vm.Close();
            this.parent.Show();
        }
        // Awaiting for maze behavior

    }
}
