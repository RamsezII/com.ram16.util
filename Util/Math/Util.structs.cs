using System;
using System.IO;
using UnityEngine;

namespace _UTIL_
{
    [Serializable]
    public struct PosRot
    {
        public Vector3 pos;
        public Quaternion rot;

        //----------------------------------------------------------------------------------------------------------

        public PosRot(in Vector3 pos, in Quaternion rot)
        {
            this.pos = pos;
            this.rot = rot;
        }

        public readonly void Deconstruct(out Vector3 pos, out Quaternion rot)
        {
            pos = this.pos;
            rot = this.rot;
        }

        public static implicit operator (Vector3 pos, Quaternion rot)(in PosRot value) => (value.pos, value.rot);

        public static implicit operator PosRot(in (Vector3 pos, Quaternion rot) value) => new(value.pos, value.rot);

        public static implicit operator PosRot(in Transform transform) => new(transform.position, transform.rotation);

        public static PosRot Slerp(in PosRot a, in PosRot b, in float lerp) => new(
            Vector3.Lerp(a.pos, b.pos, lerp),
            Quaternion.Slerp(a.rot, b.rot, lerp)
            );

        public static PosRot SlerpU(in PosRot a, in PosRot b, in float lerp) => new(
            Vector3.LerpUnclamped(a.pos, b.pos, lerp),
            Quaternion.SlerpUnclamped(a.rot, b.rot, lerp)
            );

        public readonly Vector3 TransformPoint(in Vector3 pos) => this.pos + rot * pos;
    }

    [Serializable]
    public struct PosEul : IBytes
    {
        public Vector3 pos;
        public Vector3 eul;
        public readonly Quaternion Rot => Quaternion.Euler(eul);
        public readonly Quaternion Rot_ => Quaternion.Inverse(Quaternion.Euler(eul));
        public readonly PosRot PosRot => new(pos, Rot);

        //----------------------------------------------------------------------------------------------------------

        public PosEul(in Transform T) : this(T.localPosition, T.localEulerAngles.SignedEuler()) { }
        public PosEul(in Vector3 pos, in Vector3 eul)
        {
            this.pos = pos;
            this.eul = eul;
        }

        //----------------------------------------------------------------------------------------------------------

        public static implicit operator (Vector3 pos, Vector3 eul)(in PosEul value) => (value.pos, value.eul);

        public static implicit operator PosEul(in (Vector3 pos, Vector3 eul) value) => new(value.pos, value.eul);

        public readonly void Deconstruct(out Vector3 pos, out Vector3 eul)
        {
            pos = this.pos;
            eul = this.eul;
        }

        public readonly void Deconstruct(out Vector3 pos, out Quaternion eul)
        {
            pos = this.pos;
            eul = Quaternion.Euler(this.eul);
        }

        //----------------------------------------------------------------------------------------------------------

        public void Slerp(in PosEul b, in float lerp)
        {
            pos = Vector3.Lerp(pos, b.pos, lerp);
            eul = Quaternion.Slerp(Quaternion.Euler(eul), Quaternion.Euler(b.eul), lerp).eulerAngles.SignedEuler();
        }

        public static PosEul Lerp(in PosEul a, in PosEul b, in float lerp) => new(
            Vector3.Lerp(a.pos, b.pos, lerp),
            Vector3.Lerp(a.eul, b.eul, lerp)
            );

        public static PosEul LerpUnclamped(in PosEul a, in PosEul b, in float lerp) => new(
            Vector3.LerpUnclamped(a.pos, b.pos, lerp),
            Vector3.LerpUnclamped(a.eul, b.eul, lerp)
            );

        public readonly void WriteBytes(in BinaryWriter writer)
        {
            writer.WriteV3_3f32(pos);
            writer.WriteV3_3f16(eul);
        }

        public void ReadBytes(in BinaryReader reader)
        {
            pos = reader.ReadV3_3f32();
            eul = reader.ReadV3_3f16();
        }
    }

    [Serializable]
    public struct PosDir
    {
        public Vector3 pos;
        public Vector3 dir;

        //----------------------------------------------------------------------------------------------------------

        public PosDir(in Vector3 pos, in Vector3 dir)
        {
            this.pos = pos;
            this.dir = dir;
        }

        //----------------------------------------------------------------------------------------------------------

        public static implicit operator (Vector3 pos, Vector3 dir)(in PosDir value) => (value.pos, value.dir);

        public static implicit operator PosDir(in (Vector3 pos, Vector3 dir) value) => new(value.pos, value.dir);

        public readonly void Deconstruct(out Vector3 pos, out Vector3 dir)
        {
            pos = this.pos;
            dir = this.dir;
        }

        //----------------------------------------------------------------------------------------------------------

        public readonly Vector3 Distance(in float distance) => pos + dir * distance;

        public static PosDir Slerp(in PosDir a, in PosDir b, in float slerp) => new(
            Vector3.Lerp(a.pos, b.pos, slerp),
            Vector3.Slerp(a.dir, b.dir, slerp)
            );

        public static PosDir SlerpU(in PosDir a, in PosDir b, in float slerp) => new(
            Vector3.LerpUnclamped(a.pos, b.pos, slerp),
            Vector3.SlerpUnclamped(a.dir, b.dir, slerp)
            );

        public void DrawRay(in Color color, in float duration = 0) => Debug.DrawRay(pos, dir, color, duration);
    }
}