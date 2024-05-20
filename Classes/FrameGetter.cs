using UnityEngine;

[System.Serializable]
public class FrameGetter<T>
{
    public delegate T Getter();
    public Getter getter;
    protected T value;
    int frameId = -1;

    //------------------------------------------------------------------------------------------------------------------------------

    public FrameGetter(T init, Getter getter)
    {
        value = init;
        this.getter = getter;
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public T getValue
    {
        get
        {
            if (Time.frameCount != frameId)
            {
                value = getter();
                frameId = Time.frameCount;
            }

            return value;
        }
    }
}
