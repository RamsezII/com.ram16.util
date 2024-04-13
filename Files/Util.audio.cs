using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.IO;

public static partial class Util
{
    public static IEnumerator ELoadAsAudioClip(this string path, Action<AudioClip> onClip)
    {
        string extension = Path.GetExtension(path);
        AudioType format = AudioType.WAV;

        switch (extension.ToLower())
        {
            case ".wav": format = AudioType.WAV; break;
            case ".ogg": format = AudioType.OGGVORBIS; break;
            case ".mp3": format = AudioType.MPEG; break;
            default:
                Debug.LogWarning($"Unknown audio format: \"{extension}\"");
                yield break;
        }

        UnityWebRequestAsyncOperation load;

        try
        {
            load = UnityWebRequestMultimedia.GetAudioClip("file://" + path, format).SendWebRequest();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            yield break;
        }

        while (!load.isDone)
            yield return null;

        switch (load.webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                if (Application.isPlaying)
                {
                    Debug.Log($"Loaded audio clip: \"{path}\"");
                    onClip(DownloadHandlerAudioClip.GetContent(load.webRequest));
                }
                else
                    Debug.LogWarning($"{nameof(ELoadAsAudioClip)}() called outside of play mode");
                break;

            default:
                Debug.LogWarning($"{nameof(ELoadAsAudioClip)}() failed with error: \"{load.webRequest.error}\"");
                break;
        }
    }
}