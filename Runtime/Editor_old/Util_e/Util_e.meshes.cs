#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_EDITOR_) + "/" + nameof(LogBounds))]
    static void LogBounds(MenuCommand command)
    {
        Bounds bounds = ((Transform)command.context).GetComponentInChildren<Renderer>().bounds;
        Debug.Log(bounds.max - bounds.min);
    }

    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_EDITOR_) + "/" + nameof(LogTotalBounds))]
    static void LogTotalBounds(MenuCommand command)
    {
        Bounds bounds = default;
        bool init = false;

        foreach (Renderer renderer in ((Transform)command.context).GetComponentsInChildren<Renderer>(true))
        {
            Bounds b = renderer.bounds;

            if (init)
            {
                bounds.min = Vector3.Min(bounds.min, b.min);
                bounds.max = Vector3.Max(bounds.max, b.max);
            }
            else
            {
                init = true;
                bounds.min = b.min;
                bounds.max = b.max;
            }

            Debug.DrawLine(b.center, b.min, Color.white, 5);
            Debug.DrawLine(b.center, b.max, Color.white, 5);
        }

        Debug.Log(bounds.max - bounds.min);
    }
}
#endif