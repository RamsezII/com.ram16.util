using _UTIL_;
using System;
using UnityEngine;

partial class Util
{
    public static bool TryExtractIndex_FromNGinxText(this string text, out NGinxIndex index, out string error)
    {
        string json = $"{{\"{nameof(NGinxIndex.entries)}\":{text}}}";
        return TryExtractIndex_FromNGinxJSon(json, out index, out error);
    }

    public static bool TryExtractIndex_FromNGinxJSon(this string json, out NGinxIndex index, out string error)
    {
        try
        {
            index = JsonUtility.FromJson<NGinxIndex>(json);
            error = null;
            return true;
        }
        catch (Exception e)
        {
            error = $"Failed to parse index: {e.TrimMessage()}";
            Debug.LogException(e);
            index = null;
            return false;
        }
    }
}

namespace _UTIL_
{
    [Serializable]
    public class NGinxIndex
    {
        public enum EntryTypes : byte
        {
            unknown,
            file,
            directory,
            symlink,
        }

        [Serializable]
        public class Entry
        {
            public string name;
            [SerializeField] string type, mtime;
            public int size;
            public DateTime ToDateTime => DateTime.TryParse(mtime, out DateTime date) ? date : DateTime.MinValue;
            public EntryTypes GetEntryType => Enum.TryParse(type, true, out EntryTypes entryType) ? entryType : EntryTypes.unknown;
        }

        public Entry[] entries;
    }

    /*
        [
        { "name":"ram", "type":"directory", "mtime":"Tue, 08 Apr 2025 00:09:27 GMT" },
        { "name":"test.txt", "type":"file", "mtime":"Tue, 08 Apr 2025 00:09:55 GMT", "size":117 }
        ]
    */
}