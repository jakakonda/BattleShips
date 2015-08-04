using System;

namespace BattleShips.Library
{
    [Flags]
    public enum FieldState
    {
        Unknown  = 0x00001,
        Water    = 0x00010,
        Ship     = 0x00100,
        SankShip = 0x01000,
        Hit      = 0x10000,
    }
}