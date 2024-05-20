using UnityEngine;

public static partial class Util
{
    public static TextEditor GetTE() => (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);

    public static bool IsTeInSelectionState()
    {
        TextEditor te = GetTE();
        return te.selectIndex != te.cursorIndex;
    }

    public static void SetTE(in int position) => SetTE(GetTE(), position);
    public static void SetTE(this TextEditor te, in int position)
    {
        te.cursorIndex = te.selectIndex = position;
    }

    public static void IncrementTE(in int increment) => IncrementTE(GetTE(), increment);
    public static void IncrementTE(this TextEditor te, in int increment)
    {
        te.cursorIndex += increment;
        te.selectIndex += increment;
    }
}