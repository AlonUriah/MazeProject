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
        private ISinglePlayerViewModel vm;

        public GameSurface()
        {
            InitializeComponent();
            this.vm = new VM();
            this.DataContext = this.vm;

        }


        private void GameSurface_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    this.vm.PlayerMoved(this.Name, "up");
                    break;
                case Key.Down:
                    this.vm.PlayerMoved(this.Name, "down");
                    break;
                case Key.Right:
                    this.vm.PlayerMoved(this.Name, "right");
                    break;
                case Key.Left:
                    this.vm.PlayerMoved(this.Name, "left");
                    break;
                default:
                    break;
            }

        }
        private void GameSurface_Loaded(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            if (this.Name == "player")
                parent.KeyDown += this.GameSurface_KeyDown;
            parent.MouseDown += vm.create;

        }

    }
}
