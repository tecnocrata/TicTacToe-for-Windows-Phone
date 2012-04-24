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
    public class FichaX: IFicha
    {
        
        public FichaX()
        {
            //VisualElement = CreateX();
        }

        public FichaX(Canvas container)
        {
            
        }

        private Path CreateX()
        {
            var element = new Path()
                              {
                                  Stroke = new SolidColorBrush(Colors.Red),
                                  StrokeThickness = 15,
                                  UseLayoutRounding = false
                              };
            var geometry = new PathGeometry { Figures = new PathFigureCollection() };
            var line1 = new PathFigure
            {
                Segments = new PathSegmentCollection { new LineSegment { Point = new Point(50, 50) } },
                StartPoint = new Point(0, 0)
            };
            var line2 = new PathFigure
            {
                Segments = new PathSegmentCollection { new LineSegment { Point = new Point(50, 0) } },
                StartPoint = new Point(0, 50)
            };
            geometry.Figures.Add(line1);
            geometry.Figures.Add(line2);
            element.Data = geometry;
            return element;
        }

        public Shape VisualElement { get; private set; }
        public void DrawOver(Canvas container)
        {
            VisualElement = CreateX();
            //Canvas.SetLeft(VisualElement, x);
            //Canvas.SetTop(VisualElement, y);
            Canvas.SetLeft(VisualElement, 40);
            Canvas.SetTop(VisualElement, 50);
            container.Children.Add(VisualElement);
        }
    }
}
