using UnityEngine;

partial class Util
{
    public static Vector3 TransformPoint(this Rigidbody rigidbody, in Vector3 localPoint) => rigidbody.position + rigidbody.rotation * localPoint;
    public static Vector3 InverseTransformPoint(this Rigidbody rigidbody, in Vector3 worldPoint) => Quaternion.Inverse(rigidbody.rotation) * (worldPoint - rigidbody.position);

    public static Vector3 TransformDirection(this Rigidbody rigidbody, in Vector3 localDirection) => rigidbody.rotation * localDirection;
    public static Vector3 InverseTransformDirection(this Rigidbody rigidbody, in Vector3 worldDirection) => Quaternion.Inverse(rigidbody.rotation) * worldDirection;
}