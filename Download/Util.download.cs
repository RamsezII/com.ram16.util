using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static partial class Util
{
    public static IEnumerator EDownloadFile(string localUrl, string remoteUrl, Action<bool> onSuccess)
    {
        Debug.Log($"downloading file at url: {remoteUrl.SetStyle(TextB.italic)}".ToSubLog());
        for (int i = 0; i < 10; ++i)
        {
            using UnityWebRequest www = UnityWebRequest.Get(remoteUrl);
            www.downloadHandler = new DownloadHandlerFile(localUrl);
            www.SendWebRequest();

            while (!www.isDone)
                yield return null;

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"saved file in \"{localUrl.SetStyle(TextB.italic)}\"".ToSubLog());
                onSuccess?.Invoke(true);
                yield break;
            }
            else
                Debug.LogWarning($"try: {i}, error: {www.result}, message: \"{www.error}\", url: {remoteUrl.SetColor(Colors.aqua)}");
        }
        onSuccess?.Invoke(false);
    }
}