using UnityEngine;

namespace _EDITOR_
{
    public class IKBody : MonoBehaviour
    {
        [System.Serializable]
        class IKPart
        {
            public Animator animator;
            [Range(0, 1)] public float weight;
            public AvatarIKGoal ikGoal;
            public AvatarIKHint ikHint;
            public HumanBodyBones ba = 0, bb = 0, bc = 0;
            public Transform a, b, c;

            //----------------------------------------------------------------------------------------------------------

            public void Populate()
            {
                a = new GameObject(ikGoal + "_a").transform;
                a.SetParent(animator.transform, false);

                b = new GameObject(ikGoal + "_b").transform;
                b.SetParent(a, false);

                c = new GameObject(ikGoal + "_c").transform;
                c.SetParent(a, false);

                switch (ikGoal)
                {
                    case AvatarIKGoal.LeftFoot:
                        ba = HumanBodyBones.LeftUpperLeg;
                        bb = HumanBodyBones.LeftLowerLeg;
                        bc = HumanBodyBones.LeftFoot;
                        b.localPosition = new Vector3(0, -.5f, .5f);
                        c.localPosition = new Vector3(0, -1, 0);
                        break;

                    case AvatarIKGoal.RightFoot:
                        ba = HumanBodyBones.RightUpperLeg;
                        bb = HumanBodyBones.RightLowerLeg;
                        bc = HumanBodyBones.RightFoot;
                        b.localPosition = new Vector3(0, -.5f, .5f);
                        c.localPosition = new Vector3(0, -1, 0);
                        break;

                    case AvatarIKGoal.LeftHand:
                        ba = HumanBodyBones.LeftUpperArm;
                        bb = HumanBodyBones.LeftLowerArm;
                        bc = HumanBodyBones.LeftHand;
                        b.localPosition = new Vector3(-.5f, -.5f, .5f);
                        c.localPosition = new Vector3(0, 0, 1);
                        break;

                    case AvatarIKGoal.RightHand:
                        ba = HumanBodyBones.RightUpperArm;
                        bb = HumanBodyBones.RightLowerArm;
                        bc = HumanBodyBones.RightHand;
                        b.localPosition = new Vector3(.5f, -.5f, .5f);
                        c.localPosition = new Vector3(0, 0, 1);
                        break;
                }

                a.position = animator.GetBoneTransform(ba).position;
                a.localScale *= Vector3.Distance(a.position, animator.GetBoneTransform(bc).position);
            }

            public void OnIK()
            {
                a.position = animator.GetBoneTransform(ba).position;

                animator.SetIKPosition(ikGoal, c.position);
                animator.SetIKPositionWeight(ikGoal, weight);
                animator.SetIKHintPosition(ikHint, b.position);
                animator.SetIKHintPositionWeight(ikHint, weight);
                animator.SetIKRotation(ikGoal, c.rotation);
                animator.SetIKRotationWeight(ikGoal, weight);
            }
        }

        [SerializeField] IKPart armL, armR, legL, legR;

        //----------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            Animator animator = GetComponent<Animator>();

            armL = new IKPart
            {
                animator = animator,
                weight = 1,
                ikGoal = AvatarIKGoal.LeftHand,
                ikHint = AvatarIKHint.LeftElbow,
            };

            armR = new IKPart
            {
                animator = animator,
                weight = 1,
                ikGoal = AvatarIKGoal.RightHand,
                ikHint = AvatarIKHint.RightElbow,
            };

            legL = new IKPart
            {
                animator = animator,
                ikGoal = AvatarIKGoal.LeftFoot,
                ikHint = AvatarIKHint.LeftKnee,
            };

            legR = new IKPart
            {
                animator = animator,
                ikGoal = AvatarIKGoal.RightFoot,
                ikHint = AvatarIKHint.RightKnee,
            };

            armL.Populate();
            armR.Populate();
            legL.Populate();
            legR.Populate();
        }

        //----------------------------------------------------------------------------------------------------------

        private void OnAnimatorIK(int layerIndex)
        {
            if (layerIndex == 0)
            {
                if (armL.weight != 0)
                    armL.OnIK();
                if (armR.weight != 0)
                    armR.OnIK();
                if (legL.weight != 0)
                    legL.OnIK();
                if (legR.weight != 0)
                    legR.OnIK();
            }
        }
    }
}