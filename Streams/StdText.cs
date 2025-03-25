using System.IO;
using System.Text;
using UnityEngine;

partial class Util
{
    public static float ReadFloat(this string[] lines, ref int read_i) => float.Parse(lines[read_i++]);
    public static Vector3 ReadVector3(this string[] lines, ref int read_i) => new(ReadFloat(lines, ref read_i), ReadFloat(lines, ref read_i), ReadFloat(lines, ref read_i));
}

namespace _UTIL_
{
    public class StdWriterText : StreamWriter
    {
        public StdWriterText(in string path, in bool append = false) : base(path, append, Encoding.UTF8)
        {

        }

        //----------------------------------------------------------------------------------------------------------

        public void WriteVector3(in Vector3 value) => WriteLine($"{value.x}\n{value.y}\n{value.z}");
    }
}