using UnityEngine;

public static partial class Util
{
    public static void ToggleCursor(this bool value)
    {
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
    }
}