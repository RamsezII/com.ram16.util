using UnityEngine;

public static partial class Util
{
    public static Color ModifyAlpha(this Color color, in float alpha)
    {
        color.a *= alpha;
        return color;
    }

    public static float GetHue(this Color color)
    {
        Color.RGBToHSV(color, out float h, out _, out _);
        return h;
    }

    public static Color ModifyHsv(this Color color, in float hue, float alpha)
    {
        Color.RGBToHSV(color, out _, out float s, out float v);
        color = Color.HSVToRGB(hue, s, v);
        color.a = alpha;
        return color;
    }
}