using System;
using UnityEngine;

namespace _UTIL_
{
    public class FixedStepper
    {
        [Min(0)] public float rate;
        [Range(0, 1)] public float timer;
        public Action onStep;

        //----------------------------------------------------------------------------------------------------------

        public FixedStepper(float rate, Action onStep)
        {
            this.rate = rate;
            this.onStep = onStep;
        }

        //----------------------------------------------------------------------------------------------------------

        public void Update()
        {
            timer += rate * (Time.inFixedTimeStep ? Time.fixedDeltaTime : Time.deltaTime);

            if (timer >= 1)
            {
                timer %= 1;
                onStep();
            }
        }
    }
}