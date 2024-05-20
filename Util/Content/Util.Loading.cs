using System.Threading.Tasks;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static partial class Util
{
    public static IEnumerator<AudioClip> LoadClip_async(string path)
    {
        string extension = Path.GetExtension(path);

        AudioType audioType = extension.ToLower() switch
        {
            ".wav" => AudioType.WAV,
            ".mp3" => AudioType.MPEG,
            ".ogg" => AudioType.OGGVORBIS,
            ".aif" => AudioType.AIFF,
            ".aiff" => AudioType.AIFF,
            ".xm" => AudioType.XM,
            ".mod" => AudioType.MOD,
            ".it" => AudioType.IT,
            ".s3m" => AudioType.S3M,
            _ => AudioType.UNKNOWN,
        };

        using UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, audioType);
        request.SendWebRequest();

        while (!request.isDone)
            yield return null;

        if (request.result == UnityWebRequest.Result.Success)
            yield return DownloadHandlerAudioClip.GetContent(request);
        else
            Debug.Log($"{request.result}");
    }

    public static async Task<AudioClip> LoadClip_await(string path)
    {
        AudioClip clip = null;
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            uwr.SendWebRequest();

            // wrap tasks in try/catch, otherwise it'll fail silently
            try
            {
                while (!uwr.isDone) await Task.Delay(5);
                if (uwr.isNetworkError || uwr.isHttpError)
                    Debug.Log($"{uwr.error}");
                else
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
            }
            catch (Exception err)
            {
                Debug.Log($"{err.Message}, {err.StackTrace}");
            }
        }

        return clip;
    }
}