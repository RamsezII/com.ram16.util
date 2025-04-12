using System.Text.RegularExpressions;

partial class Util
{
    public static bool TryCastEndOfLine(this string input, out int output, out string match, out int length)
    {
        Match regex = Regex.Match(input, @"\d+$");
        if (regex.Success)
        {
            match = regex.Value;
            length = regex.Length;
            output = int.Parse(match);
            return true;
        }
        else
        {
            match = null;
            length = 0;
            output = 0;
            return false;
        }
    }
}