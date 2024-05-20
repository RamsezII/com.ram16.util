using System.IO;
using System.Text;
using UnityEngine;

namespace _UTIL_
{
    public class StdReaderText : StreamReader
    {
        public StdReaderText(in string path) : base(path, Encoding.UTF8)
        {

        }

        //----------------------------------------------------------------------------------------------------------

        public bool ReadBool()
        {
            string read = ReadLine();
            return read == "1";
        }

        public int ReadInt()
        {
            string read = ReadLine();
            return int.Parse(read);
        }

        public float ReadFloat()
        {
            string read = ReadLine();
            return float.Parse(read);
        }

        public Vector2 ReadVector2() => new(ReadFloat(), ReadFloat());
        public Vector3 ReadVector3() => new(ReadFloat(), ReadFloat(), ReadFloat());
    }

    public class StdWriterText : StreamWriter
    {
        public StdWriterText(in string path, in bool append = false) : base(path, append, Encoding.UTF8)
        {

        }

        //----------------------------------------------------------------------------------------------------------

        public void WriteBool(in bool value) => WriteLine(value ? "1" : "0");
        public void WriteVector2(in Vector2 value) => WriteLine($"{value.x}\n{value.y}");
        public void WriteVector3(in Vector3 value) => WriteLine($"{value.x}\n{value.y}\n{value.z}");
    }
}