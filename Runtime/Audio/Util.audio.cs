using UnityEngine;
using UnityEngine.Audio;

public static partial class Util
{
    public static bool SetVolume_log(this AudioMixer mixer, in string parameter, in float value) => mixer.SetFloat(parameter, value > 0 ? Mathf.Log10(value) * 20 : -80);
    public static bool TryGetVolume_log(this AudioMixer mixer, in string parameter, out float value)
    {
        bool res = mixer.GetFloat(parameter, out value);
        value = Mathf.Pow(10, value / 20);
        return res;
    }

    public static void PlayRandom(this AudioSource source, in AudioClip[] clips)
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

    public static byte FloatToMuLaw(this float sample) => LinearToMuLaw((short)(sample * short.MaxValue));
    public static byte LinearToMuLaw(this short sample)
    {
        const int BIAS = 0x84;
        const int MAX = 32635;

        int sign = (sample >> 8) & 0x80;
        if (sign != 0) sample = (short)-sample;
        if (sample > MAX) sample = MAX;

        int magnitude = sample + BIAS;
        int exponent = 7;
        for (int expMask = 0x4000; (magnitude & expMask) == 0; expMask >>= 1, exponent--) { }

        int mantissa = (magnitude >> (exponent + 3)) & 0x0F;
        int muLawByte = ~(sign | (exponent << 4) | mantissa);

        return (byte)muLawByte;
    }

    public static float MuLawToFloat(this byte muLaw) => MuLawToLinear(muLaw) / (float)short.MaxValue;
    public static short MuLawToLinear(this byte muLaw)
    {
        muLaw = (byte)~muLaw;
        int sign = muLaw & 0x80;
        int exponent = (muLaw >> 4) & 0x07;
        int mantissa = muLaw & 0x0F;
        int sample = ((mantissa << 3) + 0x84) << exponent;

        return (short)(sign == 0 ? sample : -sample);
    }
}