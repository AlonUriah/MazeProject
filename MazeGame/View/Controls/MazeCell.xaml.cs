using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;


namespace MazeGame.View.Controls
{
    /// <summary>
    /// Interaction logic for MazeCell.xaml
    /// </summary>
    partial class MazeCell : UserControl
    {
        public Color Color { set; get; } = Colors.White;
        public string BackgroundImageSrc { set; get; }
        public bool IsWalkThrough { set; get; } = false;

        public MazeCell()
        {
            InitializeComponent();
            CellRectangle.Fill = new ImageBrush(new BitmapImage(new Uri(@"..\..\View\Resources\Brick.jpg", UriKind.Relative)));
        }

        public MazeCell(double height, double width, bool isWalkThrough)
        {
            InitializeComponent();
            Height = height;
            Width = width;
            IsWalkThrough = isWalkThrough;
            CellRectangle.Fill = new ImageBrush(new BitmapImage(new Uri(@"..\..\View\Resources\Logo.jpg", UriKind.Relative)));

            //var imageBrush = new ImageBrush(new BitmapImage( //(@"..\..\View\Resources\Brick.jpg"));
        }        

        private void CellRectangle_Loaded(object sender, RoutedEventArgs e)
        {
            CellRectangle.Width = Width;
            CellRectangle.Height = Height;
            CellRectangle.Fill = new ImageBrush(new BitmapImage(new Uri(@"../../View/Resources/Brick.jpg", UriKind.Relative)));
            //CellRectangle.Fill = 


            //Brush cellRectangleBrush;
            //try
            //{
            //    if (File.Exists(BackgroundImageSrc))
            //    {
            //        cellRectangleBrush = new ImageBrush(new BitmapImage(new Uri(BackgroundImageSrc, UriKind.Relative)));
            //    }
            //    else
            //    {
            //        cellRectangleBrush = new SolidColorBrush(Color);
            //    }
            //}
            //catch (Exception)
            //{
            //    cellRectangleBrush = new SolidColorBrush(Color);
            //}

            //CellRectangle.Fill = cellRectangleBrush;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
