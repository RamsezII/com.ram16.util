using UnityEngine;

namespace _UTIL_
{
    public class StateMachine
    {
        public interface IState
        {
            void Copy(in IState target);
            void Lerp(in IState buffer, in IState target, in float lerp);
        }

        [Header("~@ States @~")]
        [Range(0, 1)] public float lerp = 1;
        public float speed;
        public IState target;
        public readonly IState buffer, current;

        //----------------------------------------------------------------------------------------------------------

        public StateMachine(in IState buffer, in IState current)
        {
            this.buffer = target = buffer;
            this.current = current;
        }

        //----------------------------------------------------------------------------------------------------------

        public void CrossFade(in float speed, in IState target)
        {
            lerp = 0;
            this.speed = speed;
            this.target = target;
            buffer.Copy(current);
        }

        public void Play(in IState target)
        {
            lerp = 1;
            this.target = target;
            current.Copy(target);
        }

        public void Update(in float deltaTime)
        {
            lerp += deltaTime * speed;

            if (lerp > 1)
            {
                lerp = 1;
                current.Copy(target);
            }
            else
                current.Lerp(buffer, target, lerp);
        }
    }
}