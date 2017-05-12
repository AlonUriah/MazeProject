using MazeGame.View;
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

namespace MazeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_single_Click(object sender, RoutedEventArgs e)
        {
            Window singleplayer = new SinglePlayerMenu(this);
            this.Hide();
            singleplayer.ShowDialog();
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            Window settings = new Settings(this);
            settings.ShowDialog();
        }

        private void btn_multi_Click(object sender, RoutedEventArgs e)
        {
            Window multiplayer = new MultiplayerMenu(this);
            this.Hide();
            multiplayer.ShowDialog();
        }
    }
}
