using System;
using System.IO;

namespace _UTIL_
{
    /// <summary>Quality Of Service : bits</summary>
    enum QOSb : byte
    {
        eve,
        ack,
        reliable,
        fragmented,
        start,
        end,
        _last_
    }

    /// <summary>Quality Of Service : flags</summary>
    [Flags]
    public enum QOSf : byte
    {
        Unreliable,

        Eve = 1 << QOSb.eve,
        Ack = 1 << QOSb.ack,

        Reliable = 1 << QOSb.reliable,
        Fragment = 1 << QOSb.fragmented | Reliable,
        Start = 1 << QOSb.start,
        End = 1 << QOSb.end,

        StartFragment = Fragment | Start,
        EndFragment = Fragment | End,
        WholeFragment = Start | Fragment | End,

        _all_ = (1 << QOSb._last_) - 1,
    }

    /// <summary>positions</summary>
    public enum QOSi : byte
    {
        version,
        qos,
        id,
        attempt,
        _last_,
    }

    public static class NetSocket_util
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/" + nameof(_UTIL_) + "/" + nameof(LogQOS))]
        static void LogQOS()
        {
            System.Text.StringBuilder log = new();

            log.AppendLine($"class {nameof(QOSf)}(IntFlag):");
            for (int qos = 0; qos < (int)QOSb._last_; ++qos)
                log.AppendLine($"\t{(QOSf)(1 << qos)} = 1 << {qos}");
            log.AppendLine("");

            log.AppendLine($"class {nameof(QOSi)}(IntEnum):");
            for (QOSi qos = 0; qos < QOSi._last_; ++qos)
                log.AppendLine($"\t{qos} = {(int)qos}");
            log.AppendLine($"\tlast = {(byte)QOSi._last_}");

            UnityEngine.Debug.Log(log);
        }
#endif

        public static string LogBuffer(this byte[] buffer)
        {
            System.Text.StringBuilder log = new();
            for (int i = 0; i < (int)QOSi._last_; ++i)
                switch ((QOSi)i)
                {
                    case QOSi.version:
                        log.Append($"v.{buffer[i]} ");
                        break;
                    case QOSi.qos:
                        log.Append($"qos.{(QOSf)buffer[i]} ");
                        break;
                    case QOSi.id:
                        log.Append($"id.{buffer[i]} ");
                        break;
                    case QOSi.attempt:
                        log.Append($"att.{buffer[i]} ");
                        break;
                    default:
                        log.Append($"?.{buffer[i]} ");
                        break;
                }
            log.Append($"[ ");
            for (int i = (int)QOSi._last_; i < buffer.Length; ++i)
                log.Append($"{buffer[i]} ");
            log.Append("]");
            return log.ToString();
        }
    }

    public partial class NetSocket
    {
        public const byte shitnetVersion = 1;

        //----------------------------------------------------------------------------------------------------------

        public BinaryWriter BeginWrite(in QOSf qos, out BinaryWriter writer) => writer = BeginWrite(qos);
        public BinaryWriter BeginWrite(in QOSf qos)
        {
            stdWriter_u.BaseStream.Position = 0;
            PrefixeQOS(qos, stdWriter_u);
            return stdWriter_u;
        }

        public static void PrefixeQOS(in QOSf qos, in StdWriter writer)
        {
            for (int i = 0; i < (int)QOSi._last_; ++i)
                if (i == (int)QOSi.qos)
                    writer.Write((byte)qos);
                else
                    writer.Write((byte)0);
        }

        public static void PrefixeQOS(in QOSf qos, in byte[] buffer)
        {
            for (int i = 0; i < (int)QOSi._last_; ++i)
                buffer[i] = 0;
            buffer[(byte)QOSi.version] = shitnetVersion;
            buffer[(byte)QOSi.qos] = (byte)qos;
        }
    }
}