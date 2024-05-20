using UnityEngine;
using UnityEngine.Audio;

public static partial class Util
{
    public static bool SetVolume_log(this AudioMixer mixer, in string parameter, in float value) => mixer.SetFloat(parameter, value > 0 ? Mathf.Log10(value) * 20 : -80);
    public static bool GetVolume_log(this AudioMixer mixer, in string parameter, out float value)
    {
        bool res = mixer.GetFloat(parameter, out value);
        value = Mathf.Pow(10, value / 20);
        return res;
    }
}