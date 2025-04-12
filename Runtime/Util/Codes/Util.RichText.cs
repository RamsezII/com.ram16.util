public enum TextB : byte
{
    align,
    allcaps,
    alpha,
    /// <summary>b</summary>
    bold,
    /// <summary>cspace</summary>
    caracter_spacing,
    color,
    font,
    /// <summary>pos</summary>
    horizontal_position,
    indent,
    /// <summary>i</summary>
    italic,
    /// <summary>line-height</summary>
    line_height,
    /// <summary>line-indent</summary>
    line_indent,
    link,
    lowercase,
    margin,
    mark,
    /// <summary>mspace</summary>
    monospace,
    /// <summary>nobr</summary>
    non_breaking_spaces,
    noparse,
    /// <summary><page></summary>
    page_break,
    size,
    smallcaps,
    space,
    sprite,
    /// <summary>s</summary>
    strikethrough,
    style,
    /// <summary>sub</summary>
    subscript,
    /// <summary>sup</summary>
    superscript,
    /// <summary>u</summary>
    underline,
    uppercase,
    /// <summary>voffset</summary>
    vertical_offset,
    width,
    _last_
}

[System.Flags]
public enum TextF : int
{
    style = 1 << TextB.style,
    size = 1 << TextB.size,
    align = 1 << TextB.align,
    color = 1 << TextB.color,
    alpha = 1 << TextB.alpha,
    bold = 1 << TextB.bold,
    italic = 1 << TextB.italic,
    font = 1 << TextB.font,
    indent = 1 << TextB.indent,
    link = 1 << TextB.link,
    lowercase = 1 << TextB.lowercase,
    uppercase = 1 << TextB.uppercase,
    smallcaps = 1 << TextB.smallcaps,
    allcaps = 1 << TextB.allcaps,
    noparse = 1 << TextB.noparse,
    margin = 1 << TextB.margin,
    mark = 1 << TextB.mark,
    space = 1 << TextB.space,
    sprite = 1 << TextB.sprite,
    subscript = 1 << TextB.subscript,
    superscript = 1 << TextB.superscript,
    width = 1 << TextB.width,
    page_break = 1 << TextB.page_break,
    monospace = 1 << TextB.monospace,
    horizontal_position = 1 << TextB.horizontal_position,
    non_breaking_spaces = 1 << TextB.non_breaking_spaces,
    line_indent = 1 << TextB.line_indent,
    caracter_spacing = 1 << TextB.caracter_spacing,
    line_height = 1 << TextB.line_height,
    strikethrough = 1 << TextB.strikethrough,
    underline = 1 << TextB.underline,
    vertical_offset = 1 << TextB.vertical_offset,
    _all_ = (1 << TextB._last_) - 1,
}

public static partial class Util_richtext
{
    public static readonly string[] rtextAttr = new string[(int)TextB._last_];
    public static string ToRtext(this TextB attr) => rtextAttr[(int)attr];
    static Util_richtext()
    {
        for (TextB tb = 0; tb < TextB._last_; ++tb)
            rtextAttr[(int)tb] = tb switch
            {
                TextB.line_height => "line-height",
                TextB.bold => "b",
                TextB.italic => "i",
                TextB.caracter_spacing => "cspace",
                TextB.line_indent => "line-indent",
                TextB.monospace => "mspace",
                TextB.non_breaking_spaces => "nobr",
                TextB.page_break => "<page>",
                TextB.horizontal_position => "pos",
                TextB.strikethrough => "s",
                TextB.underline => "u",
                TextB.subscript => "sub",
                TextB.superscript => "sup",
                TextB.vertical_offset => "voffset",
                _ => "" + tb,
            };
    }
}