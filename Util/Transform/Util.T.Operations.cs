using UnityEngine;

public static partial class Util
{
    public static void Copy(this Transform transform, in Transform source)
    {
        transform.SetPositionAndRotation(source.position, source.rotation);
        transform.localScale = source.localScale;
    }

    public static void Simetrize(this Transform transform, in Vector3 mirrorPosition, in Quaternion mirrorRotation)
    {
        Quaternion _rot = Quaternion.Inverse(mirrorRotation);
        Vector3 mirror_n = mirrorRotation * Vector3.right;

        Vector3 pos = _rot * (transform.position - mirrorPosition);
        pos = Vector3.Reflect(pos, mirror_n);

        Quaternion rot = _rot * transform.rotation;
        rot = Quaternion.LookRotation(
            Vector3.Reflect(rot * Vector3.forward, mirror_n),
            Vector3.Reflect(rot * Vector3.up, mirror_n)
            );

        transform.SetPositionAndRotation(mirrorPosition + mirrorRotation * pos, mirrorRotation * rot);
    }
}