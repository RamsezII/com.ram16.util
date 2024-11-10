using System;
using System.Text.RegularExpressions;
using UnityEngine;

partial class Util
{
    public static bool TryReadTimeInSeconds(this string input, out int seconds)
    {
        try
        {
            seconds = ReadTimeInSeconds(input);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            seconds = 0;
            return false;
        }
    }

    public static int ReadTimeInSeconds(this string input)
    {
        int totalSecondes = 0;
        bool previousWasMinute = false;

        Regex regex = new(@"(\d+)(h|min|s)?", RegexOptions.IgnoreCase);
        MatchCollection correspondances = regex.Matches(input);

        foreach (Match correspondance in correspondances)
        {
            int valeur = int.Parse(correspondance.Groups[1].Value);
            string unite = correspondance.Groups[2].Success ? correspondance.Groups[2].Value.ToLower() : "";

            switch (unite)
            {
                case "h":
                    totalSecondes += valeur * 3600;
                    break;
                case "min":
                    totalSecondes += valeur * 60;
                    previousWasMinute = true;
                    break;
                case "s":
                    totalSecondes += valeur;
                    previousWasMinute = false;
                    break;
                default:
                    if (previousWasMinute)
                        totalSecondes += valeur;
                    else
                        totalSecondes += 60 * valeur;
                    break;
            }
        }

        return totalSecondes;
    }
}