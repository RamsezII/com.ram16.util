using _UTIL_;
using System;
using UnityEngine;

partial class Util
{
    public static bool TryExtractIndex_FromNGinxText(this string text, out NGinxIndex index, in Action<string> onError = null)
    {
        string json = $"{{\"{nameof(NGinxIndex.entries)}\":{text}}}";
        return TryExtractIndex_FromNGinxJSon(json, out index, onError);
    }

    public static bool TryExtractIndex_FromNGinxJSon(this string json, out NGinxIndex index, in Action<string> onError = null)
    {
        try
        {
            index = JsonUtility.FromJson<NGinxIndex>(json);
            return true;
        }
        catch (Exception e)
        {
            onError?.Invoke(e.TrimMessage());
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
        [Serializable]
        public class Entry
        {
            public string name, type;
            [SerializeField] string mtime;
            public int size;
            public DateTime ToDateTime => DateTime.TryParse(mtime, out DateTime date) ? date : DateTime.MinValue;
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