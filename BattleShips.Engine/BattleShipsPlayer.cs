using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BattleShips.Library;


namespace BattleShips.Engine
{
    public class BattleShipsPlayer
    {
        public IBattleShipPlayer Player { get; private set; }
        public FieldState[,] Board { get; set; }
        public List<Ship> Ships { get; set; }


        public BattleShipsPlayer(string name, int boardSize)
        {
            LoadPlayer(name);
            InitBoard(boardSize);
        }

        private void InitBoard(int boardSize)
        {
            var init = false;
            while (!init)
            {
                init = true;

                Board = new FieldState[boardSize, boardSize];

                for (var y = 0; y < Board.GetLength(0); y++)
                    for (var x = 0; x < Board.GetLength(1); x++)
                        Board[y, x] = FieldState.Unknown | FieldState.Water;


                Ships = Player.PlaceShips(Config.SHIP_SIZES, boardSize, boardSize);
                var sizes = Config.SHIP_SIZES.Select(o => o).ToList(); // Deep copy

                // Paint Board
                foreach (var ship in Ships)
                {
                    var hor = ship.Direction == Direction.Horizontal ? 1 : 0;
                    var ver = ship.Direction == Direction.Vertical ? 1 : 0;

                    var p = ship.Location;

                    if (p.X < 0 || p.Y < 0 ||
                        Board.GetLength(0) <= p.Y + ship.Length*ver ||
                        Board.GetLength(1) <= p.X + ship.Length*hor)
                    {
                        init = false;
                        break;
                    }
                        

                    for (int i = 0; i < ship.Length; i++)
                        Board[(int) (p.Y + i*ver), (int) (p.X + i*hor)] = FieldState.Ship | FieldState.Unknown;
                }
            }
        }


        private void LoadPlayer(string name)
        {
            var assembly = Assembly.LoadFrom($@".\Players\{name}.dll");

            foreach (var type in assembly.GetExportedTypes().Where(type => type.IsClass))
            {
                if (!type.GetInterfaces().Contains(typeof(IBattleShipPlayer)))
                    continue;

                Player = Activator.CreateInstance(type) as IBattleShipPlayer;
                break;
            }
        }


        public FieldState[,] GetBoardForEnemy()
        {
            var copy = new FieldState[Board.GetLength(0), Board.GetLength(1)];

            for (var y = 0; y < Board.GetLength(0); y++)
                for (var x = 0; x < Board.GetLength(1); x++)
                {
                    if (Board[y, x].HasFlag(FieldState.Unknown))
                        copy[y, x] = FieldState.Unknown;
                    else
                        copy[y, x] = Board[y, x] & ~FieldState.Unknown;
                }

            return copy;
        }

        public Task<Point> MakeMove(FieldState[,] enemyBoard)
        {
            Thread.Sleep(10);
            
            var cts = new CancellationTokenSource(Config.THINK_TIME);
            cts.CancelAfter(Config.THINK_TIME);

            var t = new Task<Point>(() => Player.OnMove(enemyBoard), cts.Token);
            t.Start();

            return t;
        }
    }
}