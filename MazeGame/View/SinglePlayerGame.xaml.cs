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
            this.vm.Close();
        }

        private void wdw_single_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.vm.Close();
        }
        // Awaiting for maze behavior

    }
}
