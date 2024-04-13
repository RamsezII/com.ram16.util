using _UTIL_;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace _UTIL_
{
    public enum HoldStates : byte { Down, Hold, Up }
}

public static partial class Util
{
    public static bool IsState(this ButtonControl button, in HoldStates state) => state switch
    {
        HoldStates.Down => button.wasPressedThisFrame,
        HoldStates.Hold => button.isPressed,
        HoldStates.Up => button.wasReleasedThisFrame,
        _ => throw new System.NotImplementedException($"invalid button state \"{state}\""),
    };

    public static Vector2 GetZqsd(this Keyboard device)
    {
        Vector2 value = Vector2.zero;
        if (device.wKey.isPressed)
            value.y++;
        if (device.dKey.isPressed)
            value.x++;
        if (device.sKey.isPressed)
            value.y--;
        if (device.aKey.isPressed)
            value.x--;
        return value;
    }

    public static Vector2 GetArrows(this Keyboard device)
    {
        Vector2 value = Vector2.zero;
        if (device.upArrowKey.isPressed)
            value.y++;
        if (device.rightArrowKey.isPressed)
            value.x++;
        if (device.downArrowKey.isPressed)
            value.y--;
        if (device.leftArrowKey.isPressed)
            value.x--;
        return value;
    }

    public static Vector2 GetZqsdDown(this Keyboard device)
    {
        Vector2 value = Vector2.zero;
        if (device.wKey.wasPressedThisFrame)
            value.y++;
        if (device.dKey.wasPressedThisFrame)
            value.x++;
        if (device.sKey.wasPressedThisFrame)
            value.y--;
        if (device.aKey.wasPressedThisFrame)
            value.x--;
        return value;
    }

    public static Vector2 GetArrowsDown(this Keyboard device)
    {
        Vector2 value = Vector2.zero;
        if (device.upArrowKey.wasPressedThisFrame)
            value.y++;
        if (device.rightArrowKey.wasPressedThisFrame)
            value.x++;
        if (device.downArrowKey.wasPressedThisFrame)
            value.y--;
        if (device.leftArrowKey.wasPressedThisFrame)
            value.x--;
        return value;
    }
}