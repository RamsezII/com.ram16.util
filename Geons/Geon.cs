using System;

namespace _UTIL_
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
    [Serializable]
#endif
    public abstract class Geon
    {
        static byte instances_i;
        byte instance_i;
        public string InstanceLog => instance_i + "." + instances_i;
        public override string ToString() => instance_i.ToString();

        public Geon prev, next;
        public Geon Leftest => prev == null ? this : prev.Leftest;
        public Geon Rightest => next == null ? this : next.Rightest;

        //----------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        static Geon() => instances_i = 0;
#endif
        public Geon() => instance_i = ++instances_i;

        //----------------------------------------------------------------------------------------------------------

        public virtual void OnGeon()
        {

        }

#if UNITY_EDITOR
        public string ToLog
        {
            get
            {
                System.Text.StringBuilder log = new();
                Geon geon = Leftest;
                while (geon != null)
                {
                    if (geon == this)
                        log.Append($" [{geon}] ");
                    else
                        log.Append($" {geon} ");
                    geon = geon.next;
                }
                return log.ToString();
            }
        }
#endif
    }
}