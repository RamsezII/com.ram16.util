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

        public TfmInfos(in Transform transform) : this(transform, transform.root) { }
        public TfmInfos(in Transform transform, in Transform root)
        {
            path = transform.GetPath(root);
            position = transform.localPosition;
            eulers = transform.localEulerAngles;
            scale = transform.localScale;
        }

        //----------------------------------------------------------------------------------------------------------

        public readonly Transform TryForceFindTransform(in Transform root, in bool logError = false)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                if (logError)
                    Debug.LogError($"{GetType().FullName}.{nameof(TryForceFindTransform)}() {nameof(path)} is null or empty.");
                return null;
            }

            Transform transform = root == null ? path.ForceFindTransform() : root.ForceFind(path);
            transform.SetLocalPositionAndRotation(position, Quaternion.Euler(eulers));
            transform.localScale = scale;

            return transform;
        }
    }
}