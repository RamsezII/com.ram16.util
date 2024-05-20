using System;
using UnityEngine;

namespace _UTIL_
{
    [Serializable]
    public struct TfmInfos
    {
        public string path;
        public Vector3 position, eulers, scale;

        //----------------------------------------------------------------------------------------------------------

        public TfmInfos(in string path, in Vector3 position, in Vector3 eulers, in Vector3 scale)
        {
            this.path = path;
            this.position = position;
            this.eulers = eulers;
            this.scale = scale;
        }

        public TfmInfos(in Transform transform, in Transform root)
        {
            path = transform.GetPath(root);
            position = transform.localPosition;
            eulers = transform.localEulerAngles;
            scale = transform.localScale;
        }
        
        //----------------------------------------------------------------------------------------------------------

        public Transform Apply(in Transform root)
        {
            Transform transform = root.ForceFind(path);
            transform.SetLocalPositionAndRotation(position, Quaternion.Euler(eulers));
            transform.localScale = scale;
            return transform;
        }
    }
}