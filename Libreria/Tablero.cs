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
    public class Tablero
    {
        private Recuadro[,] _recuadros;
        public Grid VisualElement { get; private set; }
        private char CurrentSymbol { get; set; }
        private int RemainingBlocks { get; set; }
        private bool FinishedGame { get; set; }
        public event EventHandler<DataEventArgs<char>> GameEnd;

        public void OnGameEnd(DataEventArgs<char> e)
        {
            EventHandler<DataEventArgs<char>> handler = GameEnd;
            if (handler != null) handler(this, e);
        }


        public Tablero(char startWith)
        {
            NewGame(startWith);
        }

        public void NewGame(char startWith)
        {
            CurrentSymbol = startWith;
            VisualElement = CreateGrid();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _recuadros = new Recuadro[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _recuadros[i, j] = new Recuadro(i, j, VisualElement);
                    _recuadros[i, j].Tap += new EventHandler<DataEventArgs<Position>>(Board_Tap);
                }
            }
            RemainingBlocks = 9;
            FinishedGame = false;
        }

        void Board_Tap(object sender, DataEventArgs<Position> e)
        {
            if (FinishedGame) return;
            if (_recuadros[e.Data.I, e.Data.J].Marked) return;

            _recuadros[e.Data.I, e.Data.J].Mark(CurrentSymbol);
            var winner = ReviewWinner(CurrentSymbol);
            if (winner != FilledLine.None)
            {
                DrawWinnerLine(winner);
                OnGameEnd(new DataEventArgs<char>(CurrentSymbol));
                FinishedGame = true;
                return;
            }
            
            CurrentSymbol = CurrentSymbol == 'X' ? 'O' : 'X';
            RemainingBlocks = RemainingBlocks - 1;
            if (RemainingBlocks == 0)
            {
                OnGameEnd(new DataEventArgs<char>(' '));
                FinishedGame = true;
                return;
            }
        }

        //<Line X1="80" X2="80" Y1="30" Y2="560" Grid.RowSpan="3"/ Stroke="#FF00FFD6" StrokeThickness="15" UseLayoutRounding="False"/>
        //<Line X1="80" X2="80" Y1="30" Y2="560" Grid.RowSpan="3" Stroke="#FF00FFD6" StrokeThickness="15" UseLayoutRounding="False" Grid.Column="1" Margin="0,-8,0,8"/>
        private void DrawWinnerLine(FilledLine winner)
        {
            if (winner == FilledLine.Vertical1)
                CreateVerticalLine(0);
            if (winner == FilledLine.Vertical2)
                CreateVerticalLine(1);
            if (winner == FilledLine.Vertical3)
                CreateVerticalLine(2);
            if (winner== FilledLine.Horizontal1)
                CreateHorizontalLine(0);
            if (winner == FilledLine.Horizontal2)
                CreateHorizontalLine(1);
            if (winner == FilledLine.Horizontal3)
                CreateHorizontalLine(2);
            if (winner == FilledLine.DiagonalRight)
                CreateDiagonalRight();
            if (winner == FilledLine.DiagonalLeft)
                CreateDiagonalLeft();
        }

        private void CreateDiagonalLeft()
        {
            var diagonal = new Path()
            {
                Stroke = new SolidColorBrush(Colors.Yellow),
                StrokeThickness = 12,
                UseLayoutRounding = false
            };
            var geometry = new LineGeometry();
            geometry.StartPoint = new Point(400, 40);
            geometry.EndPoint = new Point(20, 450);
            diagonal.Data = geometry;
            Grid.SetRowSpan(diagonal, 3);
            Grid.SetColumnSpan(diagonal, 3);
            VisualElement.Children.Add(diagonal);
        }

        private void CreateDiagonalRight()
        {
            //<Path Data="M30,47 L410.5,554.5" Canvas.Left="23" Stretch="Fill" Stroke="#FF00FFD6" StrokeThickness="15" Canvas.Top="40" UseLayoutRounding="False" Grid.ColumnSpan="3" Margin="32,46,28.5,38.5" Grid.RowSpan="3"/>
            var diagonal = new Path()
                               {
                                   Stroke = new SolidColorBrush(Colors.Yellow),
                                   StrokeThickness = 12,
                                   UseLayoutRounding = false
                               };
            var geometry = new LineGeometry();
            geometry.StartPoint= new Point(30, 47);
            geometry.EndPoint = new Point(450, 480);
            diagonal.Data = geometry;
            Grid.SetRowSpan(diagonal, 3);
            Grid.SetColumnSpan(diagonal, 3);
            VisualElement.Children.Add(diagonal);
        }

        private void CreateVerticalLine(int column)
        {
            var line = new Line()
                           {
                               X1 = 60,
                               Y1 = 30,
                               X2 = 60,
                               Y2 = 560,
                               Stroke = new SolidColorBrush(Colors.Yellow),
                               StrokeThickness = 12,
                               UseLayoutRounding = false
                           };
            Grid.SetColumn(line, column);
            Grid.SetRowSpan(line, 3);
            VisualElement.Children.Add(line);
        }

        private void CreateHorizontalLine(int row)
        {
            var line = new Line()
            {
                X1 = 20,
                Y1 = 70,
                X2 = 440,
                Y2 = 70,
                Stroke = new SolidColorBrush(Colors.Yellow),
                StrokeThickness = 12,
                UseLayoutRounding = false
            };
            Grid.SetRow(line, row);
            Grid.SetColumnSpan(line, 3);
            VisualElement.Children.Add(line);
        }

        private Grid CreateGrid()
        {
            var grid = new Grid();
            grid.ShowGridLines = true;
            //grid.Background = new SolidColorBrush(new Color() {A = 255, R = 214, G = 201, B = 66});
            grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)});
            grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)});
            grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.Children.Clear();
            return grid;
        }

        private FilledLine ReviewWinner(char symbol)
        {
            if (_recuadros[0, 0].Symbol == symbol && _recuadros[0, 1].Symbol == symbol && _recuadros[0, 2].Symbol == symbol)
                return FilledLine.Horizontal1;
            if (_recuadros[1, 0].Symbol == symbol && _recuadros[1, 1].Symbol == symbol && _recuadros[1, 2].Symbol == symbol)
                return FilledLine.Horizontal2;
            if (_recuadros[2, 0].Symbol == symbol && _recuadros[2, 1].Symbol == symbol && _recuadros[2, 2].Symbol == symbol)
                return FilledLine.Horizontal3;
            if (_recuadros[0, 0].Symbol == symbol && _recuadros[1, 0].Symbol == symbol && _recuadros[2, 0].Symbol == symbol)
                return FilledLine.Vertical1;
            if (_recuadros[0, 1].Symbol == symbol && _recuadros[1, 1].Symbol == symbol && _recuadros[2, 1].Symbol == symbol)
                return FilledLine.Vertical2;
            if (_recuadros[0, 2].Symbol == symbol && _recuadros[1, 2].Symbol == symbol && _recuadros[2, 2].Symbol == symbol)
                return FilledLine.Vertical3;
            if (_recuadros[0, 0].Symbol == symbol && _recuadros[1, 1].Symbol == symbol && _recuadros[2, 2].Symbol == symbol)
                return FilledLine.DiagonalRight;
            if (_recuadros[0, 2].Symbol == symbol && _recuadros[1, 1].Symbol == symbol && _recuadros[2, 0].Symbol == symbol)
                return FilledLine.DiagonalLeft;
            return FilledLine.None;
        }
    }

    public enum FilledLine
    {
        None,
        Horizontal1,
        Horizontal2,
        Horizontal3,
        Vertical1,
        Vertical2,
        Vertical3,
        DiagonalRight,
        DiagonalLeft
    };
}
