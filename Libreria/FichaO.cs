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
    public class FichaO: IFicha
    {
        //public Ellipse Elemeto { get; private set; }
        public FichaO()
        {
            VisualElement = CreateEllipse();
        }

        public FichaO(Canvas container)
        {
            
        }

        private static Ellipse CreateEllipse()
        {
            var Elemeto = new Ellipse();
            Elemeto.Width = 70;
            Elemeto.Height = 70;
            Elemeto.Fill = new SolidColorBrush(new Color() {A = 255, R = 0, G = 255, B = 4});
            return Elemeto;
        }

        public Shape VisualElement { get; private set; }
        public void DrawOver(Canvas container)
        {
            VisualElement = CreateEllipse();
            Canvas.SetLeft(VisualElement, 40);
            Canvas.SetTop(VisualElement, 40);
            container.Children.Add(VisualElement);
        }
    }
}
