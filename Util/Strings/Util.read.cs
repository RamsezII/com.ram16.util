partial class Util
{
    public static bool TryReadWord(this string line, out string word, out string newline, in bool trim)
    {
        if (trim)
            line = line.Trim();

        if (string.IsNullOrWhiteSpace(line))
        {
            newline = string.Empty;
            word = string.Empty;
            return false;
        }

        int end;
        for (end = 0; end < line.Length; end++)
            if (char.IsWhiteSpace(line[end]))
                break;

        word = line[..end];
        newline = line[end..];

        if (trim)
            newline = newline.Trim();

        return true;
    }
}