using System;
using System.Globalization;

[Flags]
public enum NumberStyles2 : ushort
{
    None = 0,
    AllowLeadingWhite = 1,
    AllowTrailingWhite = 2,
    AllowLeadingSign = 4,
    Integer = 7,
    AllowTrailingSign = 8,
    AllowParentheses = 16,
    AllowDecimalPoint = 32,
    AllowThousands = 64,
    Number = 111,
    AllowExponent = 128,
    Float = 167,
    AllowCurrencySymbol = 256,
    Currency = 383,
    Any = 511,
    AllowHexSpecifier = 512,
    HexNumber = 515,
    _all_ = 1 | 2 | 4 | 7 | 8 | 16 | 32 | 64 | 111 | 128 | 167 | 256 | 383 | 511,
}

public static partial class Util
{
    public const NumberStyles float_mask = (NumberStyles)NumberStyles2._all_;
    public static float ToFloat(this string read) => float.Parse(read, float_mask, CultureInfo.InvariantCulture);
    public static string FloatToString(this in float value) => value.ToString(CultureInfo.InvariantCulture);

    public static bool TryParseFloat(this string read, out float value)
    {
        try
        {
            value = ToFloat(read);
            return true;
        }
        catch
        {
            value = 0;
            return false;
        }
    }
}