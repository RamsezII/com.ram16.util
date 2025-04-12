#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Camera) + "/" + nameof(_EDITOR_) + "/" + nameof(ForceRender))]
    static void ForceRender(MenuCommand command) => ((Camera)command.context).Render();

    [MenuItem("CONTEXT/" + nameof(Camera) + "/" + nameof(CaptureScreenshot))]
    static void CaptureScreenshot()
    {
        string path = Application.streamingAssetsPath + "/screenshots";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string filename = $"{path}/screenshot - {DateTime.Now:yyyy-MM-dd hhhmmmsss}.png";
        Debug.Log($"Saving screenshot as {filename}...");
        ScreenCapture.CaptureScreenshot(filename, 1);
        Debug.Log($"Screenshot saved as {filename}");
        //ScreenCapture.CaptureScreenshot(path + "/screenshot - 0.png", 1);
    }
}
#endif