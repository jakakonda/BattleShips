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
    }
}