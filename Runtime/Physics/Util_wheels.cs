using UnityEngine;

partial class Util
{
    public static float MPS(this WheelCollider wheel) => Mathf.PI * wheel.rpm * wheel.radius / 30;
    public static float ToMPS(this in float rpm, in float radius) => Mathf.PI * rpm * radius * 30;
    public static float ToRPM(this in float mps, in float radius) => mps / (Mathf.PI * radius * 30);
}