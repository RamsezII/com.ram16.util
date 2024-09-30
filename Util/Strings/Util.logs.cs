using System;
using System.Collections;
using System.Text;

public static partial class Util
{
    public static string ToLog(this IEnumerable enumerable)
    {
        StringBuilder log = new("{ ");
        foreach (object item in enumerable)
            log.Append(item).Append(", ");
        log.Append("}");
        return log.ToString();
    }

    public static string TrimMessage(this Exception e) => e.Message.TrimEnd('\n', '\r', '\0');
    public static string FileSizeLog(this in uint fileSize) => ((long)fileSize).FileSizeLog();
    public static string FileSizeLog(this in long fileSize)
    {
        if (fileSize < 1024)
            return $"{fileSize}B";
        else if (fileSize < 1024 * 1024)
            return $"{Math.Round(fileSize / 1024f, 1)}KB";
        else if (fileSize < 1024 * 1024 * 1024)
            return $"{Math.Round(fileSize / (1024 * 1024f), 1)}MB";
        else
            return $"{Math.Round(fileSize / (1024 * 1024 * 1024f), 1)}GB";
    }

    public static string SecondsLog(this in long seconds)
    {
        if (seconds < 60)
            return $"{seconds}s";
        else if (seconds < 60 * 60)
            return $"{seconds / 60}m";
        else
            return $"{seconds / (60 * 60)}h";
    }

    public static string TimeLog(this float seconds, in bool forceMinutes = false, in bool forceHours = false)
    {
        int hours = (int)(seconds / 3600);
        int minutes = (int)(seconds % 3600 / 60);
        int remainingSeconds = (int)(seconds % 60);

        string timeLog = "";
        if (hours > 0 || forceHours)
            timeLog += $"{hours}h ";
        if (minutes > 0 || forceMinutes)
        {
            timeLog += $"{minutes:D2}m ";
            timeLog += $"{remainingSeconds:D2}s";
        }
        else
            timeLog += $"{remainingSeconds}s";

        return timeLog;
    }


    public static string FullSecondsLog(this long seconds)
    {
        if (seconds < 60)
            return $"{seconds}s";
        else if (seconds < 60 * 60)
            return $"{seconds / 60}m {seconds % 60}s";
        else
            return $"{seconds / (60 * 60)}h {seconds / 60 % 60}m {seconds % 60}s";
    }

    public static string MillisecondsLog(this in double milliseconds)
    {
        if (milliseconds < 1000)
            return $"{Math.Round(milliseconds, 1)}ms";
        else if (milliseconds < 1000 * 60)
            return $"{Math.Round(milliseconds / 1000, 1)}s";
        else if (milliseconds < 1000 * 60 * 60)
            return $"{Math.Round(milliseconds / (1000 * 60), 1)}m";
        else
            return $"{Math.Round(milliseconds / (1000 * 60 * 60), 1)}h";
    }

    public static string PercentLog(this in float lerp, int digits = 0) => $"{Math.Round(lerp * 100, digits)}%";
}