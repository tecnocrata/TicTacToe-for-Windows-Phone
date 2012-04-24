using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Libreria
{
    public class TicTacToe:Grid
    {
        private Tablero board;
        private int Jugador1 = 0;
        private int Jugador2 = 0;
        private Button btnNewGame;
        private TextBlock Score1;
        private TextBlock Score2;
        private Border border;

        public TicTacToe()
        {
            InitializeMainGrid();
            CreateFirstRow();
            CreateSecondRow();
        }

        private void CreateSecondRow()
        {
            var grid = CreateInternalGrid();
            
            CreateScoreGrid(grid);
            CreateButtonNewGame(grid);

            Grid.SetRow(grid, 1);
            this.Children.Add(grid);
        }

        private void CreateButtonNewGame(Grid grid)
        {
            btnNewGame = new Button();
            btnNewGame.FontSize = 22;
            btnNewGame.Click += new RoutedEventHandler(btnNewGame_Click);
            var txtNewGame = new TextBlock();
            LineBreak br = new LineBreak();
            txtNewGame.Inlines.Add("Nuevo");
            txtNewGame.Inlines.Add(br);
            txtNewGame.Inlines.Add("Juego");
            btnNewGame.Content = txtNewGame;
            //btnNewGame.Content = "Nuevo Juego";

            Grid.SetColumn(btnNewGame, 1);
            grid.Children.Add(btnNewGame);
        }

        private void CreateScoreGrid(Grid grid)
        {
            var scoreGrid = new Grid();
            scoreGrid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)});
            scoreGrid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)});
            scoreGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)});
            scoreGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(3, GridUnitType.Star)});
            scoreGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)});

            CreateTextBox("Jugador 1", 0, 1, scoreGrid);
            CreateTextBox("Jugador 2", 1, 1, scoreGrid);
            Score1 = CreateTextBox("0", 0, 2, scoreGrid);
            Score2 = CreateTextBox("0", 1, 2, scoreGrid);

            CreateSymbolX(scoreGrid);
            var O = new Ellipse()
                        {
                            Width = 30,
                            Height = 30,
                            Fill = new SolidColorBrush(new Color() {A = 255, R = 0, G = 255, B = 4})
                        };
            Grid.SetRow(O, 1);
            Grid.SetColumn(O, 0);
            scoreGrid.Children.Add(O);

            Grid.SetColumn(scoreGrid, 0);
            grid.Children.Add(scoreGrid);
        }

        private void CreateSymbolX(Grid scoreGrid)
        {
            var geometry = new PathGeometry {Figures = new PathFigureCollection()};
            var line1 = new PathFigure
                            {
                                Segments = new PathSegmentCollection {new LineSegment {Point = new Point(25, 25)}},
                                StartPoint = new Point(0, 0)
                            };
            var line2 = new PathFigure
                            {
                                Segments = new PathSegmentCollection {new LineSegment {Point = new Point(25, 0)}},
                                StartPoint = new Point(0, 25)
                            };
            geometry.Figures.Add(line1);
            geometry.Figures.Add(line2);
            Path X = new Path()
                         {
                             Stroke = new SolidColorBrush(Colors.Red),
                             StrokeThickness = 10,
                             UseLayoutRounding = false,
                             Data = geometry,
                             VerticalAlignment = VerticalAlignment.Center,
                             HorizontalAlignment = HorizontalAlignment.Center
                         };
            Grid.SetRow(X, 0);
            Grid.SetColumn(X, 0);
            scoreGrid.Children.Add(X);
        }

        private TextBlock CreateTextBox(string label, int row, int column, Grid scoreGrid)
        {
            var lblGamer1 = new TextBlock()
                                {
                                    Text = label,
                                    FontSize = 30,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    VerticalAlignment = VerticalAlignment.Center
                                };
            Grid.SetRow(lblGamer1, row);
            Grid.SetColumn(lblGamer1, column);
            scoreGrid.Children.Add(lblGamer1);
            return lblGamer1;
        }

        void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            board.NewGame('X');
            border.Child = board.VisualElement;
        }

        private Grid CreateInternalGrid()
        {
            var grid = new Grid();
            grid.ShowGridLines = false;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.Children.Clear();
            return grid;
        }

        private void CreateFirstRow()
        {
            border = new Border()
                             {
                                 BorderThickness = new Thickness(20)
                             };
            board= new Tablero('X');
            board.GameEnd += new EventHandler<DataEventArgs<char>>(board_GameEnd);
            border.Child = board.VisualElement;
            Grid.SetRow(border, 0);
            this.Children.Add(border);
        }

        void board_GameEnd(object sender, DataEventArgs<char> e)
        {
            if (e.Data == 'X')
            {
                Jugador1++;
                MessageBox.Show("Gano Jugador 1");
            }
            else if (e.Data == 'O')
            {
                Jugador2++;
                MessageBox.Show("Gano Jugador 2");
            }
            else
            {
                Jugador1++;
                Jugador2++;
                MessageBox.Show("Empate");
            }
            Score1.Text = Jugador1.ToString();
            Score2.Text = Jugador2.ToString();

        }

        private void InitializeMainGrid()
        {
            ShowGridLines = false;
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        }
    }
}
