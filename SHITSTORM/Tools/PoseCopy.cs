#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class PoseCopy : MonoBehaviour
{
    Dictionary<Transform, Vector3> positions;
    Dictionary<Transform, Quaternion> rotations;

    [ContextMenu("CopyPose")]
    void CopyPose()
    {
        positions = new Dictionary<Transform, Vector3>();
        rotations = new Dictionary<Transform, Quaternion>();

        Animator anm = GetComponent<Animator>();
        Transform T = anm.GetBoneTransform(HumanBodyBones.Hips);

        while (T.parent != anm.transform)
            T = T.parent;

        foreach (Transform bone in T.GetComponentsInChildren<Transform>())
        {
            positions[bone] = bone.position;
            rotations[bone] = bone.rotation;
        }
    }

    [ContextMenu("PastePose")]
    void PastePose()
    {
        foreach (Transform bone in positions.Keys)
            bone.SetPositionAndRotation(positions[bone], rotations[bone]);
    }

    private void Awake()
    {
        Destroy(this);
    }
}
#endif