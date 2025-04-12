#if UNITY_EDITOR
using UnityEngine;

partial class Util_e
{
    public static void DrawCOG(this Rigidbody rigidbody) => DrawCOG(rigidbody, Color.red);
    public static void DrawCOG(this Rigidbody rigidbody, Color color)
    {
        Vector3 cog = rigidbody.worldCenterOfMass;
        Gizmos.color = .5f * color;
        Gizmos.DrawSphere(cog, .1f);
        Gizmos.color = .3f * color;
        Gizmos.DrawWireSphere(cog, .2f);
    }
}
#endif