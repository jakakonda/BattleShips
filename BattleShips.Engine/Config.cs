using System.Collections.Generic;
using System.Windows.Media;

namespace BattleShips.Engine
{
    public class Config
    {
        public static readonly List<int> SHIP_SIZES = new List<int> {5, 4, 3, 3, 2};

        public static readonly Brush LINE_COLOR =   new SolidColorBrush(Colors.DeepSkyBlue);
        public const int LINE_THICKNESS = 1;

        public static readonly Brush SHIP_OK_COLOR = new SolidColorBrush(Colors.DarkGray);
        public static readonly Brush SHIP_HIT_COLOR = new SolidColorBrush(Colors.Orange);
        public static readonly Brush SHIP_SANK_COLOR = new SolidColorBrush(Colors.DarkRed);
        public static readonly Brush SHIP_STROKE = new SolidColorBrush(Colors.Gray);

        public static int THINK_TIME = 100; //ms
        public static int BETWEEN_TURNS = 250; //ms
    }
}