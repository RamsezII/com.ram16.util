using UnityEngine;

namespace _UTIL_
{
    public class IKSolver
    {
        readonly Transform a, b, c;
        readonly Quaternion IK2A, IK2B;

        [HideInInspector] public Vector3 pos_a;
        [HideInInspector] public float ab, bc, at;

        //----------------------------------------------------------------------------------------------------------

        public IKSolver(in Transform a, in Transform b, in Transform c, in Quaternion initRot)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            IK2A = Quaternion.Inverse(Quaternion.LookRotation(b.position - a.position, initRot * Vector3.up)) * a.rotation;
            IK2B = Quaternion.Inverse(Quaternion.LookRotation(c.position - b.position, initRot * Vector3.up)) * b.rotation;
        }

        //----------------------------------------------------------------------------------------------------------

        public void Solve_scaled(in Vector3 hint, in Vector3 target, in float slerp)
        {
            pos_a = a.position;

            Vector3
                bpos = a.InverseTransformPoint(b.position),
                cpos = a.InverseTransformPoint(c.position),
                tpos = a.InverseTransformPoint(target);

            ab = bpos.magnitude;
            bc = Vector3.Distance(bpos, cpos);
            at = tpos.magnitude;

            Solve(pos_a, ab, bc, at, hint, target, slerp);
        }
        
        public void Solve_unscaled(in Vector3 hint, in Vector3 target, in float slerp)
        {
            pos_a = a.position;

            Vector3 
                pos_b = b.position,
                pos_c = c.position;

            ab = Vector3.Distance(pos_a, pos_b);
            bc = Vector3.Distance(pos_b, pos_c);
            at = Vector3.Distance(pos_a, target);

            Solve(pos_a, ab, bc, at, hint, target, slerp);
        }

        void Solve(in Vector3 pos_a, in float ab, in float bc, in float at, in Vector3 hint, in Vector3 target, in float lerp)
        {
            Util.SolveIK(at, ab, bc, out float angleA, out _, out float angleC);

            Quaternion
                rotc = c.rotation,
                rot = Quaternion.LookRotation(target - pos_a, hint - pos_a),
                rota = rot * Quaternion.Euler(-angleA, 0, 0) * IK2A,
                rotb = Quaternion.Inverse(rota) * rot * Quaternion.Euler(angleC, 0, 0) * IK2B;

            a.rotation = Quaternion.Slerp(a.rotation, rota, lerp);
            b.localRotation = Quaternion.Slerp(b.localRotation, rotb, lerp);
            c.rotation = rotc;
        }
    }
}