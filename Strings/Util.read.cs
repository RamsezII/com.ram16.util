using UnityEngine;

partial class Util
{
    public static bool TryReadWord(this string line, out string word, out string newline, in bool trim = true)
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

    public static bool TryReadFloat(this string line, out float value, out string newline)
    {
        try
        {
            if (line.TryReadWord(out string word, out newline))
                if (float.TryParse(word, out value))
                    return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"{typeof(Util).FullName}.{nameof(TryReadFloat)}: \"{e.Message}\"");
        }

        value = 0;
        newline = string.Empty;
        return false;
    }

    public static bool TryReadVector3(this string line, out Vector3 value, out string newline)
    {
        value = Vector3.zero;

        try
        {
            if (line.TryReadWord(out string word, out newline))
                if (float.TryParse(word, out value.x))
                    if (newline.TryReadWord(out word, out newline))
                        if (float.TryParse(word, out value.y))
                            if (newline.TryReadWord(out word, out newline))
                                if (float.TryParse(word, out value.z))
                                    return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"{typeof(Util).FullName}.{nameof(TryReadVector3)}: \"{e.Message}\"");
        }

        newline = string.Empty;
        return false;
    }
}