using System.Collections.Generic;

namespace _UTIL_
{
    public class RedundancyQueue<T> : Queue<T>
    {
        readonly byte capacity;

        //----------------------------------------------------------------------------------------------------------

        public RedundancyQueue(in byte capacity) : base(capacity) => this.capacity = capacity;

        //----------------------------------------------------------------------------------------------------------

        public bool RedundancyCheck(in T value)
        {
            if (Contains(value))
                return true;
            else
            {
                if (Count >= capacity - 1)
                    Dequeue();
                Enqueue(value);
                return false;
            }
        }
    }
}