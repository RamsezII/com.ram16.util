namespace _UTIL_
{
    /// <summary>
    /// \uxxxx for a unicode character hex value(e.g. \u0020).
    /// \x is the same as \u, but you don't need leading zeroes (e.g. \x20).
    /// \Uxxxxxxxx for a unicode character hex value(longer form needed for generating surrogates).
    /// </summary>
    public enum SChars : byte
    {
        single_quote = (byte)'\'',
        double_quote = (byte)'\"',
        backslah = (byte)'\\',
        _null_ = 0,
        alert = (byte)'\a',
        backspace = (byte)'\b',
        form_feed = (byte)'\f',
        new_line = (byte)'\n',
        carriage_return = (byte)'\r',
        horizontal_tab = (byte)'\t',
        vertical_tab = (byte)'\v',
    }
}