using MazeGame.ViewModel;
using MazeGame.ViewModel.Interfaces;
using MazeLib;
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
    /// Interaction logic for GameSurface.xaml
    /// </summary>
    public partial class GameSurface : UserControl
    {
        public string Key { get; set; } = string.Empty;

       

        public string Player
        {
            get { return (string)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Player.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerProperty =
            DependencyProperty.Register("Player", typeof(string), typeof(GameSurface), new PropertyMetadata(null));


        public GameSurface()
        {
            InitializeComponent();
        }

    }
}
