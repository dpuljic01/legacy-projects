using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whack_a_Mouse
{
    static class GameOptions
    {
        public static int ItemHeight = 100;
        public static int ItemWidth = 100;
        public static int Speed = 1;//brzina izlaska miša na početku

        public static int LeftEdge = 0;
        public static int RightEdge = 800;
        public static int UpEdge = 0;
        public static int DownEdge = 600;

        public static int TimeLeft = 60;//početak odbrojavanja(sekunde)
    }
}
