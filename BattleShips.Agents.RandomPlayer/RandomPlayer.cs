using System.Windows;
using BattleShips.Library;

namespace BattleShips.Agents
{
    public class RandomPlayer : BattleShipPlayerBase
    {
        public override Point OnMove(FieldState[,] board)
        {
            var p = Helpers.RandomPoint(Height, Width);

            while (!board[(int) p.Y, (int) p.X].HasFlag(FieldState.Unknown))
                p = Helpers.RandomPoint(Height, Width);

            return p;
        }

        public override string Name()
        {
            return "Random Boom Boom";
        }
    }
}
