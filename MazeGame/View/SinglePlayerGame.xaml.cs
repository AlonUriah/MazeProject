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
            if (this.player.Name == "player")
                this.KeyDown += this.GameSurface_KeyDown;
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
            this.vm.PlayerMoved(this.Name, this.player.Key);
        }

        private void btn_restart_Click(object sender, RoutedEventArgs e)
        {
            this.vm.Restart();
        }

        private void btn_solve_Click(object sender, RoutedEventArgs e)
        {
            this.vm.Solve();
            this.btn_solve.IsEnabled = false;
            this.btn_restart.IsEnabled = false;
        }

        private void btn_quit_Click(object sender, RoutedEventArgs e)
        {
            if (this.btn_solve.IsEnabled)
                this.vm.Close();
            this.Close();
        }

        private void wdw_single_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.btn_solve.IsEnabled)
                this.vm.Close();
        }
        // Awaiting for maze behavior

    }
}
