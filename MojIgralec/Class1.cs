using System.Windows;
using BattleShips.Library;

namespace MojIgralec
{
    public class MojIgralec : BattleShipPlayerBase
    {
        public override string Name()
        {
            return "Podmornica";
        }

        public override Point OnMove(FieldState[,] board)
        {
            var tocka = Helpers.RandomPoint(Height, Width);

            while (!board[(int)tocka.Y, (int)tocka.X].HasFlag(FieldState.Unknown))
                tocka = Helpers.RandomPoint(Height, Width);

            return tocka;
        }
    }
}
