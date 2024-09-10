using System.IO;

namespace _UTIL_
{
    public abstract class SettingsFile : JSon
    {
        public string FILE_PATH => Path.Combine(Util.HOME_DIR.FullName, GetType().FullName + json);

        //--------------------------------------------------------------------------------------------------------------

        public void Save(in bool log) => Save(FILE_PATH, log);
        public static void Load<T>(ref T text, in bool log) where T : SettingsFile, new()
        {
            text = new();
            Read(ref text, text.FILE_PATH, true, log);
        }
    }
}