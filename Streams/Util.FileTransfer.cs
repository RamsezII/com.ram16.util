using System.IO;
using System;
using UnityEngine;

public static partial class Util_net
{
    [Obsolete]
    public static bool TryDownloadFile(this BinaryReader reader, in string savePath, in bool log, out FileInfo savedFile, in Action<string> onProgression)
    {
        uint fileSize = reader.ReadUInt32(), remaining = fileSize;
        if (remaining > 0)
        {
            string folderPath = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            onProgression?.Invoke($"downloading: {savePath} ({remaining.FileSizeLog()})".ToSubLog());

            using FileStream fs = new(savePath, FileMode.Create);
            byte[] buffer = new byte[TCP_CHUNK];

            while (remaining > 0)
            {
                ushort chunk_size = (ushort)(remaining > TCP_CHUNK ? TCP_CHUNK : remaining);
                ushort actual_size = (ushort)reader.Read(buffer, 0, chunk_size);
                remaining -= actual_size;
                if (actual_size > 0)
                    fs.Write(buffer, 0, actual_size);
                else
                {
                    Debug.LogWarning("msgLen == 0");
                    break;
                }

                onProgression?.Invoke($"downloading: {savePath} ({(1 - (float)remaining / fileSize).PercentLog()})".ToSubLog());
            }
            fs.Close();

            onProgression?.Invoke(null);
            if (log)
                Debug.Log($"downloaded: {savePath}".ToSubLog());

            savedFile = new(savePath);
            return true;
        }
        else
        {
            savedFile = null;
            return false;
        }
    }

    [Obsolete]
    public static void UploadFile(this BinaryWriter writer, in FileInfo file, in bool log, in Action<string> onProgression)
    {
        uint fileSize = (uint)file.Length, remaining = fileSize;
        writer.Write(remaining);

        if (remaining > 0)
        {
            onProgression?.Invoke($"uploading: {file.FullName} ({remaining.FileSizeLog()})".ToSubLog());

            using FileStream fs = file.OpenRead();
            byte[] buffer = new byte[TCP_CHUNK];

            while (remaining > 0)
            {
                ushort sendLength = (ushort)(remaining > TCP_CHUNK ? TCP_CHUNK : remaining);
                ushort actualLength = (ushort)fs.Read(buffer, 0, sendLength);
                remaining -= actualLength;
                writer.Write(buffer, 0, actualLength);

                onProgression?.Invoke($"uploading: {file.FullName} ({(1 - (float)remaining / fileSize).PercentLog()})".ToSubLog());
            }
            fs.Close();

            onProgression?.Invoke(null);
            if (log)
                Debug.Log($"uploaded: {file.FullName}".ToSubLog());
        }
    }
}