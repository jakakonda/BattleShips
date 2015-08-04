using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BattleShips.Library
{
    public abstract class BattleShipPlayerBase : IBattleShipPlayer
    {
        protected int Height;
        protected int Width;

        public List<Ship> PlaceShips(List<int> sizes, int boardHeight, int boardWidth)
        {
            Height = boardHeight;
            Width = boardWidth;

            var ships = new List<Ship>();

            for (int i = 0; i < sizes.Count; i++)
            {
                var size = sizes[i];

                // Create new ship
                var newShip = new Ship
                {
                    Direction = Helpers.Random.Next(0, 1 + 1) == 0 ? Direction.Horizontal : Direction.Vertical,
                    Length = size,
                    Location = Helpers.RandomPoint(boardHeight, boardWidth)
                };

                // Check that it doesn't overlap with any other
                bool ok = ships.All(ship => !ship.Overlaps(newShip));

                // Do not advance to next ship, 
                // repeat the same one next iteration 
                if (!ok || 
                    newShip.EndPoint.X > boardWidth || 
                    newShip.EndPoint.Y >= boardHeight)
                    i--;
                else
                    ships.Add(newShip);
            }

            return ships;
        }

        public virtual void OnEnemyMove(Point position)
        {
            // Enemy just shot!

            // Use this method if you wan't to track enemy shoots
            // Pretty much useless
            // But is here of anyone want's to track
        }

        public abstract Point OnMove(FieldState[,] board);

        public abstract string Name();
    }
}
