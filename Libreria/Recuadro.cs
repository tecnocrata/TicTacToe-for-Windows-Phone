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
    public class Recuadro 
    {
        public bool Marked { get; set; }
        public char Symbol{ get; set; }
        public Canvas VisualElement { get; set; }
        public event EventHandler<DataEventArgs<Position> > Tap;

        public void OnTap(DataEventArgs<Position> e)
        {
            EventHandler<DataEventArgs<Position>> handler = Tap;
            if (handler != null) handler(this, e);
        }

        private IFicha ficha;
        public Position Position { get; set; }
        

        public Recuadro(int i, int j , Grid container)
        {
            Symbol = ' ';
            Marked = false;

            VisualElement = new Canvas();
            Position = new Position() {I = i, J = j};
            VisualElement.Tag = i + "-" + j;
            VisualElement.Background = new SolidColorBrush(new Color() { A = 0, R = 0, G = 0, B = 0 });
            VisualElement.Tap += new EventHandler<GestureEventArgs>(VisualElement_Tap);
            Grid.SetRow(VisualElement, i);
            Grid.SetColumn(VisualElement, j);
            container.Children.Add(VisualElement);
        }

        void VisualElement_Tap(object sender, GestureEventArgs e)
        {
            OnTap(new DataEventArgs<Position>(Position));
        }

        public void Mark (char symbol)
        {
            if (symbol=='X') ficha= new FichaX();
            else ficha= new FichaO();
            Symbol = symbol;
            Marked = true;
            ficha.DrawOver(VisualElement);
        }
    }

    public class DataEventArgs<T>:EventArgs
    {
        public T Data { get; set; }
        public DataEventArgs(T data)
        {
            Data = data;
        }
    }

    public class Position
    {
        public int I { get; set; }
        public int J { get; set; }
    }
}
