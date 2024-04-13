using UnityEngine;

public enum ColorsB : byte
{
    ///<summary>same as cyan</summary>
    aqua,
    black,
    blue,
    brown,
    ///<summary>same as aqua</summary>
    cyan,
    darkblue,
    ///<summary>same as magenta</summary>
    fuchsia,
    green,
    grey,
    lightblue,
    lime,
    ///<summary>same as fuchsia</summary>
    magenta,
    maroon,
    navy,
    olive,
    orange,
    purple,
    red,
    silver,
    teal,
    white,
    yellow,
    _last_,
}

public enum Colors : uint
{
    aqua = 0x00ffffff,
    black = 0x000000ff,
    blue = 0x0000ffff,
    brown = 0xa52a2aff,
    cyan = 0x00ffffff,
    darkblue = 0x0000a0ff,
    fuchsia = 0xff00ffff,
    green = 0x008000ff,
    grey = 0x808080ff,
    lightblue = 0xadd8e6ff,
    lime = 0x00ff00ff,
    magenta = 0xff00ffff,
    maroon = 0x800000ff,
    navy = 0x000080ff,
    olive = 0x808000ff,
    orange = 0xffa500ff,
    purple = 0x800080ff,
    red = 0xff0000ff,
    silver = 0xc0c0c0ff,
    teal = 0x008080ff,
    white = 0xffffffff,
    yellow = 0xffff00ff,
}

public static partial class Util
{
    public static Color GetColor(this Colors color) => throw new System.Exception("not yet");
}