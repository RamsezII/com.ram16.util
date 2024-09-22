using System.IO;
using UnityEngine;

namespace _UTIL_
{
    public abstract class SettingsFile : JSon
    {
        public DirectoryInfo FILE_DIR => Path.Combine(Application.streamingAssetsPath, typeof(SettingsFile).FullName).GetDir(true);
        public string FILE_PATH => Path.Combine(FILE_DIR.FullName, GetType().FullName).ToSafePath() + json;

        //--------------------------------------------------------------------------------------------------------------

        public void Save(in bool log) => Save(FILE_PATH, log);
        public static void Load<T>(ref T text, in bool log) where T : SettingsFile, new()
        {
            text = new();
            Read(ref text, text.FILE_PATH, true, log);
        }
    }
}