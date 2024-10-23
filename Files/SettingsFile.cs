using System.IO;
using UnityEngine;

namespace _UTIL_
{
    public abstract class SettingsFile : JSon
    {
        public string FILE_PATH => Path.Combine(Path.Combine(Application.streamingAssetsPath, typeof(SettingsFile).TypeToPath()).GetDir(true).FullName, GetType().TypeToPath()) + json;

        //--------------------------------------------------------------------------------------------------------------

        public void Save(in bool log) => Save(FILE_PATH, log);
        public static void Load<T>(ref T text, in bool log) where T : SettingsFile, new()
        {
            text = new();
            Read(ref text, text.FILE_PATH, true, log);
        }
    }
}