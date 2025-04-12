using UnityEngine;

namespace _UTIL_
{
    public static partial class Typing
    {
        public static byte pointsCount;

        //----------------------------------------------------------------------------------------------------------

        public static void UpdatePoints(in float scale = 3)
        {
            byte count = (byte)(scale * Time.unscaledTime % 4);

            if (pointsCount != count)
            {
                SetFlags(TypingFlags.points);
                pointsCount = count;
            }
        }

        public static string getPoints
        {
            get
            {
                switch (pointsCount)
                {
                    case 0: return ".";
                    case 1: return "..";
                    case 2: return "...";
                    default: return "";
                }
            }
        }
    }
}