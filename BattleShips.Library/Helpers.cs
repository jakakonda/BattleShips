using System;
using System.Windows;

namespace BattleShips.Library
{
    public static class Helpers
    {
        public static readonly Random Random = new Random();

        public static Point RandomPoint(int maxY, int maxX)
        {
            return new Point(Random.Next(0, maxX), Random.Next(0, maxY));
        }

        private static Point Rotate(Point source, double cosine, double sine)
        {
            return new Point(source.X * cosine + source.Y * sine, source.Y * cosine - source.X * sine);
        }

        public static Point Intersection(Point p1, Point p2, Point p3, Point p4)
        {
            if (p1.X == p3.X && p2.X == p4.X)
            {
                if (Math.Min(p2.X, p4.X) - Math.Max(p1.X, p3.X) >= 0)
                    return new Point(0, 0);
            }
            if (p1.Y == p3.Y && p2.Y == p4.Y)
            {
                if (Math.Min(p2.Y, p4.Y) - Math.Max(p1.Y, p3.Y) >= 0)
                    return new Point(0, 0);

            }
            // Store the values for fast access and easy
            // equations-to-code conversion
            double X1 = p1.X, X2 = p2.X, X3 = p3.X, X4 = p4.X;
            double Y1 = p1.Y, Y2 = p2.Y, Y3 = p3.Y, Y4 = p4.Y;

            double d = (X1 - X2) * (Y3 - Y4) - (Y1 - Y2) * (X3 - X4);

            // If d is zero, there is no intersection
            if (d == 0)
                return new Point(-1, -1);

            // Get the X and Y
            double pre = (X1 * Y2 - Y1 * X2), post = (X3 * Y4 - Y3 * X4);
            double X = (pre * (X3 - X4) - (X1 - X2) * post) / d;
            double Y = (pre * (Y3 - Y4) - (Y1 - Y2) * post) / d;

            // Check if the X and Y coordinates are within both lines
            if (X < Math.Min(X1, X2) || X > Math.Max(X1, X2) ||
                X < Math.Min(X3, X4) || X > Math.Max(X3, X4))
                return new Point(-1, -1);

            if (Y < Math.Min(Y1, Y2) || Y > Math.Max(Y1, Y2) ||
                Y < Math.Min(Y3, Y4) || Y > Math.Max(Y3, Y4))
                return new Point(-1, -1);

            return new Point(X, Y);
        }

        public static bool Intersect(Point a, Point b, Point c, Point d)
        {
            var point = Intersection(a, b, c, d);

            if (point.X == -1 && point.Y == -1)
                return false;

            //if (double.IsNaN(point.X))
            //    return true;

            //if (Math.Min(a.X, b.X) <= point.X && point.X <= Math.Max(a.X, b.X) &&
            //    Math.Min(a.Y, b.Y) <= point.Y && point.Y <= Math.Max(a.Y, b.Y) &&
            //    Math.Min(c.X, d.X) <= point.X && point.X <= Math.Max(c.X, d.X) &&
            //    Math.Min(c.Y, d.Y) <= point.Y && point.Y <= Math.Max(c.Y, b.Y))
            //    return true;


            return true;
        }
    }
}