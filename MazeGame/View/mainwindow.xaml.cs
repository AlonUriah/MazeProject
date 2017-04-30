using System.Windows;

namespace MazeGame.View
{
    /// <summary>
    /// Interaction logic for mainwindow.xaml
    /// </summary>
    public partial class mainwindow : Window
    {
        public mainwindow()
        {
            InitializeComponent();
        }

        private void d_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayer ss = new SinglePlayer();
            ss.Show();
            this.Close();
        }
    }
}
