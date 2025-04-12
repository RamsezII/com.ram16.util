using System.Collections.Generic;
using UnityEngine;

namespace _EDITOR_
{
    public class TPoser : MonoBehaviour
    {
#if UNITY_EDITOR
        public Transform root;

        public bool memorize, apply, destroy;

        Dictionary<Transform, Quaternion> rots;
        Dictionary<Transform, Vector3> pos;

        private void OnValidate()
        {
            if (memorize)
            {
                memorize = false;

                rots = new Dictionary<Transform, Quaternion>();
                pos = new Dictionary<Transform, Vector3>();

                if (!root)
                    root = transform;

                foreach (Transform bone in root.GetComponentsInChildren<Transform>())
                {
                    rots[bone] = bone.rotation;
                    pos[bone] = bone.position;
                }
            }

            if (apply)
            {
                apply = false;

                foreach (KeyValuePair<Transform, Quaternion> bone in rots)
                    bone.Key.rotation = bone.Value;

                foreach (KeyValuePair<Transform, Vector3> bone in pos)
                    bone.Key.position = bone.Value;
            }

            if (destroy)
            {
                destroy = false;
                this.Destroy();
            }
        }
#endif

        private void Awake() => Destroy(this);
    }
}