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


    }
}
