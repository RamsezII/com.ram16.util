using System;
using UnityEngine;

namespace _UTIL_
{
    [Serializable]
    public struct SphereColliderInfos
    {
        public string path;
        public Vector3 center;
        public float radius;

        //----------------------------------------------------------------------------------------------------------

        public SphereColliderInfos(in SphereCollider cld)
        {
            path = cld.transform.GetPath(false);
            center = cld.center;
            radius = cld.radius;
        }

        public void Apply(in SphereCollider cld)
        {
            cld.center = center;
            cld.radius = radius;
        }
    }

    [Serializable]
    public struct CapsuleColliderInfos
    {
        public string path;
        public Vector3 center, euler;
        public float radius;
        public float height;
        public int direction;

        //----------------------------------------------------------------------------------------------------------

        public CapsuleColliderInfos(in CapsuleCollider cld)
        {
            path = cld.transform.GetPath(false);
            center = cld.center;
            euler = cld.transform.localEulerAngles;
            radius = cld.radius;
            height = cld.height;
            direction = cld.direction;
        }

        public void Apply(in CapsuleCollider cld)
        {
            cld.center = center;
            cld.transform.localEulerAngles = euler;
            cld.radius = radius;
            cld.height = height;
            cld.direction = direction;
        }
    }

    [Serializable]
    public struct BoxColliderInfos
    {
        public string path;
        public Vector3 center, euler, size;

        //----------------------------------------------------------------------------------------------------------

        public BoxColliderInfos(in BoxCollider cld)
        {
            path = cld.transform.GetPath(false);
            center = cld.center;
            euler = cld.transform.localEulerAngles;
            size = cld.size;
        }

        public void Apply(in BoxCollider cld)
        {
            cld.center = center;
            cld.transform.localEulerAngles = euler;
            cld.size = size;
        }
    }
}