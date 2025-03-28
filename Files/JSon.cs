using System.IO;
using UnityEngine;

public abstract class JSon
{
    public const Colors color_paths = Colors.light_yellow;

    public const string
        txt = ".txt",
        json = ".json" + txt;

    public bool readflag { get; private set; }

    //----------------------------------------------------------------------------------------------------------

    public string ToJson() => JsonUtility.ToJson(this, true);
    protected virtual void OnApply() { }
    protected virtual void OnSave() => OnApply();
    public void Save(in string filepath, in bool log)
    {
        OnSave();
        if (log)
            Debug.Log($"{typeof(JSon).FullName}.{nameof(Save)}<{GetType()}>({filepath.SetColor(color_paths)})".ToSubLog());
        Save(filepath, JsonUtility.ToJson(this, true), false);
    }

    public static void Save(in string filepath, in string text, in bool log)
    {
        filepath.CheckParentDirectory();

        if (File.Exists(filepath))
            File.SetAttributes(filepath, FileAttributes.Normal);

        File.WriteAllText(filepath, text);
        File.SetAttributes(filepath, FileAttributes.Normal);

        if (log)
            Debug.Log($"{typeof(JSon).FullName}.{nameof(Save)}({filepath.SetColor(color_paths)})".ToSubLog());
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

    public static bool Read<T>(ref T json, in string filepath, in bool force, in bool log) where T : JSon
    {
        if (File.Exists(filepath))
        {
            json = JsonUtility.FromJson<T>(File.ReadAllText(filepath));
            json.OnRead();
            if (log)
                Debug.Log($"{typeof(JSon).FullName}.{nameof(Read)}<{typeof(T)}>({filepath.SetColor(color_paths)})".ToSubLog());
            return true;
        }
        else
        {
            if (force)
            {
                json.Save(filepath, true);
                json.OnRead();
                return true;
            }
            else
                Debug.LogWarning($"can not read or find file at path: {filepath.SetColor(color_paths)}");
            return false;
        }
    }
}