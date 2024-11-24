using System;

namespace _UTIL_
{
    [Serializable]
    public readonly struct ShockwaveValues
    {
        public readonly float force, lifetime, speed, treshold, offset;

        //----------------------------------------------------------------------------------------------------------

        public ShockwaveValues(in float force, in float lifetime, in float treshold, in float offset = 0)
        {
            this.force = force;
            this.lifetime = lifetime;
            speed = 1 / lifetime;
            this.treshold = treshold;
            this.offset = offset;
        }

        //----------------------------------------------------------------------------------------------------------

        public readonly bool TryReadValue(float localTime, out float value)
        {
            localTime -= offset;
            localTime *= speed;
            if (localTime > 0 && localTime < 1)
            {
                value = force * treshold.EvaluateRecoilCurve(localTime);
                return true;
            }
            else
            {
                value = 0;
                return false;
            }
        }
    }
}