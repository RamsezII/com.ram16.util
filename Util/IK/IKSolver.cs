using UnityEngine;

namespace _UTIL_
{
    public class IKSolver
    {
        readonly Transform a, b, c;
        readonly Quaternion IK2A, IK2B;

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

        public void Solve_scaled(in Vector3 hint, in Vector3 target, in float weight)
        {
            Vector3 pos_a = a.position;

            Vector3
                bpos = a.InverseTransformPoint(b.position),
                cpos = a.InverseTransformPoint(c.position),
                tpos = a.InverseTransformPoint(target);

            float ab = bpos.magnitude;
            float bc = Vector3.Distance(bpos, cpos);
            float at = tpos.magnitude;

            Solve(pos_a, ab, bc, at, hint, target, weight);
        }

        public void Solve_unscaled(in Vector3 hint, in Vector3 target, in float weight)
        {
            Vector3
                pos_a = a.position,
                pos_b = b.position,
                pos_c = c.position;

            float
                ab = Vector3.Distance(pos_a, pos_b),
                bc = Vector3.Distance(pos_b, pos_c),
                at = Vector3.Distance(pos_a, target);

            float safeZone = 1.01f * (ab - bc);
            if (at < safeZone)
                at = safeZone;

            Solve(pos_a, ab, bc, at, hint, target, weight);
        }

        void Solve(in Vector3 pos_a, in float ab, in float bc, in float at, in Vector3 hint, in Vector3 target, in float weight)
        {
            Util.SolveIK(at, ab, bc, out float angleA, out _, out float angleC);

            Quaternion
                rot = Quaternion.LookRotation(target - pos_a, hint - pos_a),
                rota = rot * Quaternion.Euler(-angleA, 0, 0) * IK2A,
                rotb = Quaternion.Inverse(rota) * rot * Quaternion.Euler(angleC, 0, 0) * IK2B;

            Quaternion arot = a.rotation = Quaternion.Slerp(a.rotation, rota, weight);
            b.localRotation = Quaternion.Slerp(b.localRotation, Quaternion.Inverse(b.parent.rotation) * arot * rotb, weight);
        }
    }
}