using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using BattleShips.Library;

namespace BattleShips.Engine
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game
    {
        private const int BORAD_SIZE = 10;
        private readonly List<Canvas> _boards = new List<Canvas>();
        private readonly List<BattleShipsPlayer> _players = new List<BattleShipsPlayer>();
        private readonly Timer _timer;
        private int _turn = 0;

        public Game()
        {
            InitializeComponent();

            _boards.Add(BoardOne);
            _boards.Add(BoardTwo);

            LoadPlayers();

            _timer = new Timer
            {
                Enabled = true,
                Interval = Config.BETWEEN_TURNS
            };
            _timer.Elapsed += TimerOnElapsed; 
        }

        private async void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var curr = _players[_turn%2];
            var oppo = _players[(_turn + 1)%2];

            var p = await curr.MakeMove(oppo.GetBoardForEnemy());

            if (oppo.Board[(int) p.Y, (int) p.X].HasFlag(FieldState.Unknown))
            {
                oppo.Board[(int) p.Y, (int) p.X] = oppo.Board[(int) p.Y, (int) p.X] & ~FieldState.Unknown;

                foreach (var ship in oppo.Ships)
                {
                    var hor = ship.Direction == Direction.Horizontal ? 1 : 0;
                    var ver = ship.Direction == Direction.Vertical   ? 1 : 0;

                    var pos = ship.Location;

                    var ok = true;
                    for (int i = 0; i < ship.Length; i++)
                    {
                        if (oppo.Board[(int) (pos.Y + i*ver), (int) (pos.X + i*hor)].HasFlag(FieldState.Ship) &&
                            !oppo.Board[(int) (pos.Y + i*ver), (int) (pos.X + i*hor)].HasFlag(FieldState.Unknown)) ;
                        else
                        {
                            ok = false;
                            break;
                        }
                    }

                    if(ok)
                        for (int i = 0; i < ship.Length; i++)
                            oppo.Board[(int)(pos.Y + i * ver), (int)(pos.X + i * hor)] = FieldState.SankShip;
                }
            }
            else ;
                // ???

            Dispatcher.Invoke(InvalidateVisual, DispatcherPriority.Render);

            _turn++;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            
            for(int i = 0; i < _players.Count; i++)
            {
                var player = _players[i];
                var board = _boards[i];

                board.Children.Clear();

                DrawBoard(board);
                DrawShips(board, player);
                DrawShipBorders(board, player);
            }
        }


        private void DrawShipBorders(Canvas canvas, BattleShipsPlayer player)
        {
            var size = Math.Min(canvas.ActualHeight, canvas.ActualWidth);
            var rectSize = size / BORAD_SIZE;

            foreach (var ship in player.Ships)
            {
                canvas.DrawEmptyRect(ship.StartPoint.X*rectSize, ship.StartPoint.Y*rectSize,
                    ship.EndPoint.X*rectSize + rectSize, ship.EndPoint.Y*rectSize + rectSize);
            }
        }


        private void DrawShips(Canvas canvas, BattleShipsPlayer player)
        {
            var board = player.Board;
            var size = Math.Min(canvas.ActualHeight, canvas.ActualWidth);
            var rectSize = size / BORAD_SIZE;

            for (var y = 0; y < board.GetLength(0); y++)
                for (var x = 0; x < board.GetLength(1); x++)
                {
                    if (board[y, x].HasFlag(FieldState.Water) && !board[y, x].HasFlag(FieldState.Unknown))
                        canvas.DrawMiss(x * rectSize, y * rectSize, (x+1) * rectSize, (y+1) * rectSize);



                    if (board[y, x].HasFlag(FieldState.Ship) && !board[y, x].HasFlag(FieldState.Unknown))
                        canvas.DrawRect(x * rectSize, y * rectSize, rectSize, rectSize, Config.SHIP_HIT_COLOR);

                    else if (board[y, x].HasFlag(FieldState.SankShip))
                        canvas.DrawRect(x * rectSize, y * rectSize, rectSize, rectSize, Config.SHIP_SANK_COLOR);

                    else if (board[y, x].HasFlag(FieldState.Ship))
                        canvas.DrawRect(x* rectSize, y* rectSize, rectSize, rectSize, Config.SHIP_OK_COLOR);
                }
        }


        private void LoadPlayers()
        {
            _players.Add(new BattleShipsPlayer("RandomPlayer", BORAD_SIZE));
            _players.Add(new BattleShipsPlayer("RandomPlayer", BORAD_SIZE));
        }


        private void DrawBoard(Canvas board)
        {
            var size = Math.Min(board.ActualHeight, board.ActualWidth);

            var rectSize = size/BORAD_SIZE;

            for (int i = 1; i <= BORAD_SIZE; i++)
            {
                // Vertical
                board.DrawLine(i*rectSize, 0, i * rectSize, BORAD_SIZE*rectSize);

                // Horizontal
                board.DrawLine(0, i*rectSize, BORAD_SIZE * rectSize, i*rectSize);
            }
        }
    }
}
