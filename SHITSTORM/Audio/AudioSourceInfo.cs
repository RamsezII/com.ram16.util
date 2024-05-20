using UnityEngine;

[System.Serializable]
public class AudioSourceInfo
{
    public AudioClip clip;
    public bool loop;
    public float stereo = 1;
    public float volume = 1;
    public float dopplerLevel = 1;
    [Tooltip("min, max")] public Vector2 spread = new Vector2(1, 500);
}