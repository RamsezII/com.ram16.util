namespace _UTIL_
{
    public static partial class Screens
    {
        public enum SplitB : byte
        {
            topLeft,
            topRight,
            downLeft,
            downRight,
        }

        public enum SplitF : byte
        {
            topLeft = 1 << SplitB.topLeft,
            topRight = 1 << SplitB.topRight,
            downLeft = 1 << SplitB.downLeft,
            downRight = 1 << SplitB.downRight,

            top = topLeft | topRight,
            right = topRight | downRight,
            down = downRight | downLeft,
            left = downLeft | topLeft,

            all = topLeft | topRight | downRight | downLeft,
        }

        //----------------------------------------------------------------------------------------------------------


    }
}