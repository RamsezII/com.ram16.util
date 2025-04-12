using UnityEngine;

public static partial class Util
{
    public static void DrawTransform_gizmos(in Vector3 position, in Quaternion rotation, in float scale)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, rotation * Vector3.right * scale);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, rotation * Vector3.up * scale);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(position, rotation * Vector3.forward * scale);
    }

    public static void DrawTransform_debug(in Vector3 position, in Quaternion rotation, in float scale, in float timer = 0)
    {
        Debug.DrawRay(position, rotation * Vector3.right * scale, Color.red, timer);
        Debug.DrawRay(position, rotation * Vector3.up * scale, Color.green, timer);
        Debug.DrawRay(position, rotation * Vector3.forward * scale, Color.blue, timer);
    }

    public static void DrawSphereGizmo(this in Vector3 position, in float radius, in Color color, in bool force = false)
    {
        if (!force && position == default)
            return;

        Gizmos.color = color;
        Gizmos.DrawSphere(position, .5f * radius);
        Gizmos.DrawWireSphere(position, radius);
    }
}