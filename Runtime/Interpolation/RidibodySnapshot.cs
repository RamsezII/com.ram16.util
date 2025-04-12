using System;
using System.IO;
using UnityEngine;

namespace _UTIL_
{
    [Serializable]
    public readonly struct RidibodySnapshot
    {
        public readonly float time;
        public readonly Vector3 pos, vlc, avlc;
        public readonly Quaternion rot;

        //----------------------------------------------------------------------------------------------------------

        public RidibodySnapshot(in Rigidbody rigidbody)
        {
            time = Time.time;
            pos = rigidbody.position;
            vlc = rigidbody.linearVelocity;
            avlc = rigidbody.angularVelocity;
            rot = rigidbody.rotation;
        }

        public RidibodySnapshot(in BinaryReader reader)
        {
            time = Time.time;
            pos = reader.ReadV3_3f32();
            vlc = reader.ReadV3_3f16();
            avlc = reader.ReadV3_3f16();
            rot = Quaternion.Euler(reader.ReadV3_3f16());
        }

        //----------------------------------------------------------------------------------------------------------

        public readonly void WriteBytes(in BinaryWriter writer)
        {
            writer.WriteV3_3f32(pos);
            writer.WriteV3_3f16(vlc);
            writer.WriteV3_3f16(avlc);
            writer.WriteV3_3f16(rot.eulerAngles);
        }

        public readonly Vector3 PredictPos(in RidibodySnapshot next, in float time, in float lerp)
        {
            return Vector3.Lerp(pos + vlc * (time - this.time), next.pos + next.vlc * (time - next.time), lerp);
        }

        public readonly Quaternion PredictRot(in RidibodySnapshot next, in float time, in float lerp)
        {
            return Quaternion.Euler(Vector3.Lerp(avlc * (time - this.time), next.avlc * (time - next.time), lerp)) * Quaternion.Slerp(rot, next.rot, lerp);
        }
    }
}