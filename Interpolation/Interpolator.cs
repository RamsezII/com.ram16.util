using System;
using UnityEngine;

namespace _UTIL_
{
    public abstract class Interpolator<T> where T : struct
    {
        public readonly struct Value
        {
            public readonly T value;
            public readonly float time;

            public Value(in T value, in float time)
            {
                this.value = value;
                this.time = time;
            }

            public void Deconstruct(out T value, out float time)
            {
                value = this.value;
                time = this.time;
            }

            public static implicit operator (T value, float time)(in Value value) => (value.value, value.time);

            public static implicit operator Value(in (T value, float time) value) => new(value.value, value.time);
        }

        public Value a, b;
        public bool Diff => a.time != b.time;

        //----------------------------------------------------------------------------------------------------------

        public void OnValue(in T value, in float time) => OnValue((value, time));
        public void OnValue(in Value value)
        {
            a = b;
            b = value;
        }

        public T Interp(in float time) => Lerp(Mathf.InverseLerp(a.time, b.time, time));
        protected abstract T Lerp(in float lerp);

        public T InterpU(in float time) => LerpU(Util.InverseLerpUnclamped(a.time, b.time, time));
        protected abstract T LerpU(in float lerpU);
    }

    [Serializable]
    public class InterpolatorV3 : Interpolator<Vector3>
    {
        protected override Vector3 Lerp(in float lerp) => Vector3.Lerp(a.value, b.value, lerp);
        protected override Vector3 LerpU(in float lerpU) => Vector3.LerpUnclamped(a.value, b.value, lerpU);
    }

    [Serializable]
    public class InterpolatorPosRot : Interpolator<PosRot>
    {
        protected override PosRot Lerp(in float lerp) => PosRot.Slerp(a.value, b.value, lerp);
        protected override PosRot LerpU(in float lerpU) => PosRot.SlerpU(a.value, b.value, lerpU);
    }

    [Serializable]
    public class InterpolatorPosDir : Interpolator<PosDir>
    {
        protected override PosDir Lerp(in float lerp) => PosDir.Slerp(a.value, b.value, lerp);
        protected override PosDir LerpU(in float lerpU) => PosDir.SlerpU(a.value, b.value, lerpU);
    }

    [Serializable]
    public class InterpolatorRgb1 : Interpolator<RgbInfos1>
    {
        protected override RgbInfos1 Lerp(in float lerp) => new(
            Vector3.Lerp(a.value.position, b.value.position, lerp),
            Vector3.Lerp(a.value.velocity, b.value.velocity, lerp)
            );

        protected override RgbInfos1 LerpU(in float lerpU) => new(
            Vector3.LerpUnclamped(a.value.position, b.value.position, lerpU),
            Vector3.LerpUnclamped(a.value.velocity, b.value.velocity, lerpU)
            );

        public Vector3 GetPos(in float time) => Interp(time).GetPos(time - b.time);
        public Vector3 GetPosU(in float time) => InterpU(time).GetPos(time - b.time);
    }

    [Serializable]
    public readonly struct RgbInfos1
    {
        public readonly Vector3 position;
        public readonly Vector3 velocity;
        public Vector3 GetPos(in float delta) => position + velocity * delta;

        //----------------------------------------------------------------------------------------------------------

        public RgbInfos1(in Rigidbody rigidbody) : this(rigidbody.position, rigidbody.velocity) { }
        public RgbInfos1(in Vector3 position, in Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        //----------------------------------------------------------------------------------------------------------

        public static implicit operator (Vector3 position, Vector3 velocity)(in RgbInfos1 value) => (value.position, value.velocity);

        public static implicit operator RgbInfos1(in (Vector3 position, Vector3 velocity) value) => new(value.position, value.velocity);

        public void Deconstruct(out Vector3 position, out Vector3 velocity)
        {
            position = this.position;
            velocity = this.velocity;
        }
    }
}