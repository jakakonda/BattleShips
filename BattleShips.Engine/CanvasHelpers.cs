using System;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using BattleShips.Library;

namespace BattleShips.Engine
{
    public static class CanvasHelpers
    {
        public static void DrawLine(this Canvas canvas, double x1, double y1, double x2, double y2)
        {
            var line = new Line
            {
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                StrokeThickness = Config.LINE_THICKNESS,
                Stroke = Config.LINE_COLOR
            };

            canvas.Children.Add(line);
        }


        public static void DrawRect(this Canvas canvas, double x, double y, double height, double width, Brush brush)
        {
            var rect = new Rectangle
            {
                Height = height,
                Width = width,
                Fill = brush,
            };

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            canvas.Children.Add(rect);
        }


        public static void DrawEmptyRect(this Canvas canvas, double x1, double y1, double x2, double y2)
        {
            var rect = new Rectangle()
            {
                Width = Math.Abs(x1 - x2),
                Height = Math.Abs(y1 - y2),
                StrokeThickness = 2,
                Stroke = Config.SHIP_STROKE,
            };


            Canvas.SetLeft(rect, Math.Min(x1, x2));
            Canvas.SetTop(rect, Math.Min(y1, y2));

            canvas.Children.Add(rect);
        }


        public static void DrawMiss(this Canvas canvas, double x1, double y1, double x2, double y2)
        {
            var line1 = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                StrokeThickness = 3,
                Stroke = new SolidColorBrush(Colors.White)
            };



            var line2 = new Line
            {
                X1 = x2,
                Y1 = y1,
                X2 = x1,
                Y2 = y2,
                StrokeThickness = 3,
                Stroke = new SolidColorBrush(Colors.White)
            };

            canvas.Children.Add(line1);
            canvas.Children.Add(line2);
        }
    }
}