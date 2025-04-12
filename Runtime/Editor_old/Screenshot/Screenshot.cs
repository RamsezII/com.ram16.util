#if UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;

namespace _EDITOR_
{
    internal class Screenshot : MonoBehaviour
    {
        [SerializeField] string savefolder;

        //--------------------------------------------------------------------------------------------------------------

        [ContextMenu(nameof(CaptureScreenshot))]
        void CaptureScreenshot()
        {
            string path = savefolder.ForceDir().FullName;
            string filepath = Path.Combine(path, $"screenshot__{DateTime.Now:yyyy_MM_dd_hhhmmmsss}.png");
            Debug.Log($"Saving screenshot as {filepath}...");
            ScreenCapture.CaptureScreenshot(filepath, 1);
            Debug.Log($"Screenshot saved as {filepath}");
            //ScreenCapture.CaptureScreenshot(path + "/screenshot - 0.png", 1);
        }
    }
}
#endif