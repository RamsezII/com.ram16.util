namespace _UTIL_
{
    //https://en.wikipedia.org/wiki/Block_Elements
    public enum TypingBars : byte { bar_middle, block_left, block_full, block_lower, terminal, tchat, }

    public enum TypingBits : byte { refresh, changes, points }

    [System.Flags]
    public enum TypingFlags : byte
    {
        refresh = 1 << TypingBits.refresh, 
        changes = 1 << TypingBits.changes, 
        points = 1 << TypingBits.points,
    }

    public static partial class Typing
    {
        static TypingFlags flags;

        public static float bar_time;
        public static bool bar_toggle;

        //----------------------------------------------------------------------------------------------------------

        public static string GetValue(in TypingBars barType) =>
            bar_toggle ? value + GetBarChar(barType) : value;

        public static char GetBarChar(in TypingBars barType)
        {
            switch (barType)
            {
                case TypingBars.terminal:
                case TypingBars.block_left: return '▌';
                case TypingBars.block_full: return '█';
                case TypingBars.block_lower: return '▂';
                case TypingBars.tchat:
                default: return '|';
            }
        }

        public static bool GetFlags(in TypingFlags mask) => (mask & flags) == mask;
        public static void SetFlags(in TypingFlags mask) => flags |= mask;

        public static bool PullFlags(in TypingFlags mask)
        {
            if (GetFlags(mask))
            {
                flags &= ~mask;
                return true;
            }
            else
                return false;
        }
    }
}