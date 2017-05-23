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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private Window parent;

        public Settings(Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.DataContext = Properties.Settings.Default;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            // Update settings

            Properties.Settings.Default.Save();

            this.Close();
            parent.Show();
        }

        private void wdw_settings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Reload();
            parent.Show();
        }
    }
}
