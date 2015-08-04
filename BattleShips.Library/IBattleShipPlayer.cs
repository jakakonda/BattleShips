using System.Collections.Generic;
using System.Windows;

namespace BattleShips.Library
{
    public interface IBattleShipPlayer
    {
        List<Ship> PlaceShips(List<int> sizes, int boardHeight, int boardWidth);

        void OnEnemyMove(Point position);
        Point OnMove(FieldState[,] board);

        string Name();
    }
}
