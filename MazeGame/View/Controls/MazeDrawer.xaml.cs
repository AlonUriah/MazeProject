using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Input;
using MazeGame.View.Events;


namespace MazeGame.View.Controls
{
    public delegate void IPlayerMovedEventHandler(object sender, PlayerMovedEventArgs e);
    /// <summary>
    /// Interaction logic for MazeDrawer.xaml
    /// </summary>
    public partial class MazeDrawer : UserControl
    {
        private readonly Dictionary<Tuple<int,int>, Label> _cells = new Dictionary<Tuple<int,int>, Label>();

        const char WALL_SIGN = '0';
        const char FREE_SIGN = '1';
        const char START_SIGN = 'S';
        const char GOAL_SIGN = 'G';
        const char PLAYER_SIGN = 'P';

        public event IPlayerMovedEventHandler OnPlayerMoved;

        public int Rows { set; get; } = 3;
        public int Cols { set; get; } = 3;
        public string PlayerBackgroundColor { set; get; } = "Red";
        public string WallBackgroundColor { set; get; } = "Black";
        public string FreeCellBackgroundColor { set; get; } = "White";
        public string GoalCellBackgroundColor { set; get; } = "Gold";

        public string PlayerImgSrc { set; get; }        
        public string WallImgSrc { set; get; }        
        public string FreeCellImgSrc { set; get; }
        public string GoalCellImgSrc { set; get; }
        public string MazeString { set; get; } = "111111111";

        private Tuple<int, int> _currentLocation = new Tuple<int, int>(0, 0);

        private Brush GetBackgroundForCell(char cellSign)
        {
            string color;
            string imageSource;
            
            if (cellSign == WALL_SIGN)
            {
                color = WallBackgroundColor;
                imageSource = WallImgSrc;
            }
            else if(cellSign == FREE_SIGN)
            {
                color = FreeCellBackgroundColor;
                imageSource = FreeCellImgSrc;
            }
            else if(cellSign == START_SIGN || cellSign == PLAYER_SIGN)
            {
                color = PlayerBackgroundColor;
                imageSource = PlayerImgSrc;
            }
            else
            {
                color = GoalCellBackgroundColor;
                imageSource = GoalCellImgSrc;
            }

            /*if (File.Exists(imageSource))
            {
                return new ImageBrush(new BitmapImage(new Uri(imageSource,UriKind.Relative)));
            }*/

            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
        }
        
        public MazeDrawer()
        {
            InitializeComponent();
            
        }

        public void Player_Moved_Event(object sender, PlayerMovedEventArgs e)
        {
            Label oldPlace = _cells[e.OldLocation];
            Label newPlace = _cells[e.NewLocation];

            oldPlace.Background = GetBackgroundForCell(FREE_SIGN);
            newPlace.Background = GetBackgroundForCell(PLAYER_SIGN);
        }

        private void MazeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var columnDefinitions = MazeGrid.ColumnDefinitions;
            var rowDefinitions = MazeGrid.RowDefinitions;

            char cellSign;
            for (int row = 0; row < Rows; row++)
            {
                rowDefinitions.Add(new RowDefinition());
                rowDefinitions[row].Height = new GridLength(30);//new GridLength(30, GridUnitType.Star);

                for (int col = 0; col < Cols; col++)
                {
                    columnDefinitions.Add(new ColumnDefinition());
                    columnDefinitions[col].Width = new GridLength(30);//, GridUnitType.Star);

                    cellSign = MazeString[(row * Cols) + col];
                    var labelId = string.Format("cell{0}{1}", row, col);

                    var label = new Label();
                    label.Name = labelId;
                    label.Background = GetBackgroundForCell(cellSign);
                    label.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                    label.Margin = new Thickness(2, 2, 2, 2);
                    RegisterName(label.Name, label);
                    label.SetValue(Grid.RowProperty, row);
                    label.SetValue(Grid.ColumnProperty, col);
                    label.HorizontalAlignment = HorizontalAlignment.Stretch;
                    label.VerticalAlignment = VerticalAlignment.Stretch;

                    //var cell = new MazeCell(30,30,(cellSign == WALL_SIGN));
                    //cell.Name = labelId;
                    ////cell.Background = GetBackgroundForCell(cellSign);
                    //cell.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                    //cell.Margin = new Thickness(2, 2, 2, 2);
                    //RegisterName(cell.Name, cell);
                    //cell.SetValue(Grid.RowProperty, row);
                    //cell.SetValue(Grid.ColumnProperty, col);
                    //cell.HorizontalAlignment = HorizontalAlignment.Stretch;
                    //cell.VerticalAlignment = VerticalAlignment.Stretch;

                    MazeGrid.Children.Add(label);// cell);
                    _cells[new Tuple<int, int>(row, col)] = label;// cell;
                }
            }
        }

        private void MazeGrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    break;
                case Key.Down:
                    break;
                case Key.Left:
                    break;
                case Key.Right:
                    break;
                default:
                    break;
            }
        }
    }
}
