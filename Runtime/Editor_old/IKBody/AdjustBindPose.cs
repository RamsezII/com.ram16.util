using System;
using UnityEngine;

namespace _EDITOR_
{
    internal class AdjustBindPose : MonoBehaviour
    {
        [SerializeField] SkinnedMeshRenderer skinnedMesh; // Le SkinnedMeshRenderer contenant l'armature
        [SerializeField] Transform sourceBone; // La nouvelle rotation souhaitée
        [SerializeField] Transform targetBone; // Le bone à ajuster

        //--------------------------------------------------------------------------------------------------------------

        [ContextMenu(nameof(AdjustBindPose))]
        void AdjustBoneBindPose()
        {
            Quaternion desiredRotation = sourceBone.rotation;
            Mesh mesh = skinnedMesh.sharedMesh;
            Matrix4x4[] bindPoses = mesh.bindposes;
            Transform[] bones = skinnedMesh.bones;

            // Recherche l'index du bone dans la liste des bones
            int boneIndex = Array.IndexOf(bones, targetBone);
            if (boneIndex == -1)
            {
                Debug.LogError("Bone non trouvé dans le SkinnedMeshRenderer !");
                return;
            }

            // Réoriente la matrice bind pose
            Matrix4x4 boneMatrix = targetBone.localToWorldMatrix;
            Matrix4x4 parentMatrix = targetBone.parent.localToWorldMatrix.inverse;
            Matrix4x4 newBindPose = parentMatrix * Matrix4x4.TRS(targetBone.localPosition, desiredRotation, targetBone.localScale);

            bindPoses[boneIndex] = newBindPose;

            // Applique les nouvelles bind poses
            mesh.bindposes = bindPoses;

            Debug.Log("Bind pose du bone mise à jour !");
        }
    }
}