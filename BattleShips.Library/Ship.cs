using System;
using System.Windows;

namespace BattleShips.Library
{
    public class Ship
    {
        public Point Location;
        public Direction Direction;
        public int Length;

        public Point StartPoint
        {
            get
            {
                return Location;
            }
        }

        public Point EndPoint
        {
            get
            {
                var x = Direction == Direction.Horizontal ? Length - 1  : 0;
                var y = Direction == Direction.Vertical ? Length - 1 : 0;
                return new Point(Location.X + x, Location.Y + y);
            }
        }

        public bool Overlaps(Ship p)
        {
            return Helpers.Intersect(StartPoint, EndPoint, p.StartPoint, p.EndPoint);
        }

        public bool Neighbour(Ship p)
        {
            if (p.Direction     == Direction.Vertical &&
                this.Direction  == Direction.Vertical &&
                Math.Abs(p.StartPoint.X - this.StartPoint.X) == 1 &&
                Math.Min(p.EndPoint.Y, this.EndPoint.Y)+1 - Math.Max(p.StartPoint.Y, this.StartPoint.Y) > 0)
                return true;

            if (p.Direction     == Direction.Horizontal &&
                this.Direction  == Direction.Horizontal &&
                Math.Abs(p.StartPoint.Y - this.StartPoint.Y) == 1 &&
                Math.Min(p.EndPoint.X, this.EndPoint.X)+1 - Math.Max(p.StartPoint.X, this.StartPoint.X) > 0)
                return true;

            var ver = this.Direction == Direction.Vertical ? this : p;
            var hor = this.Direction == Direction.Vertical ? p    : this;

            if (ver.StartPoint.Y <= hor.StartPoint.Y && hor.StartPoint.Y <= ver.EndPoint.Y &&
                Math.Min(Math.Abs(ver.StartPoint.X - hor.StartPoint.X), Math.Abs(ver.StartPoint.X - hor.EndPoint.X)) == 1
                 ||
                hor.StartPoint.X <= ver.StartPoint.X && ver.StartPoint.X <= hor.EndPoint.X &&
                Math.Min(Math.Abs(hor.StartPoint.Y - ver.StartPoint.Y), Math.Abs(hor.StartPoint.Y - ver.EndPoint.Y)) == 1)
                return true;

            return false;
        }
    }
}