using System.IO;
using UnityEngine;

public abstract class JSon
{
    public const string
        txt = ".txt",
        json = ".json" + txt;
    public bool readflag { get; private set; }

    //----------------------------------------------------------------------------------------------------------

    public string ToJson() => JsonUtility.ToJson(this, true);
    protected virtual void OnApply() { }
    protected virtual void OnSave() => OnApply();
    public void Save(in string filepath, in FileAttributes attributes = FileAttributes.Normal)
    {
        OnSave();
        Save(filepath, JsonUtility.ToJson(this, true), false, attributes);
    }

    public static void Save(in string filepath, in string text, in bool log, in FileAttributes attributes)
    {
        filepath.CheckParentDirectory();

        if (File.Exists(filepath))
            File.SetAttributes(filepath, FileAttributes.Normal);

        File.WriteAllText(filepath, text);
        File.SetAttributes(filepath, attributes);

        Debug.Log($"{nameof(JSon)}.{nameof(Save)}: {filepath}".ToSubLog());
    }

    public virtual void OnRead()
    {
        readflag = true;
        OnApply();
    }

    public virtual void WriteBytes(in BinaryWriter writer)
    {

    }

    public virtual void ReadBytes(in BinaryReader reader)
    {

    }

    //----------------------------------------------------------------------------------------------------------

    public static bool TryRead<T>(out T json, in string filepath, in bool force, in bool log) where T : JSon
    {
        json = null;
        return Read(ref json, filepath, force, log, false);
    }

    public static bool Read<T>(ref T json, in string filepath, in bool force, in bool log, in bool logError) where T : JSon
    {
        if (File.Exists(filepath))
        {
            json = JsonUtility.FromJson<T>(File.ReadAllText(filepath));
            json.OnRead();
            if (log)
                Debug.Log($"{nameof(JSon)}.{nameof(Read)}: {filepath}".ToSubLog());
            return true;
        }
        else
        {
            if (logError)
            {
                string message = $"can not read or find file at path: {filepath}";
                if (force)
                    Debug.LogWarning(message);
                else
                    Debug.LogError(message);
            }

            if (force)
                json.Save(filepath);
            return false;
        }
    }
}