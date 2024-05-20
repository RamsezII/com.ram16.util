using System.IO;
using System.Net.Sockets;
using System.Text;
using System;
using UnityEngine;
using System.Threading.Tasks;

public static partial class Util_net
{
    public const ushort TCP_CHUNK = 1024;

    //----------------------------------------------------------------------------------------------------------

    public static void TryOpenParagonTCP(Action<TcpClient, BinaryWriter, BinaryReader> onCommunication, bool logs = true, Action onError = null) => TryOpenSocketTCP_async(DOMAIN_3VE, PORT_PARAGON, onCommunication, logs, onError);
    public static void TryOpenSocketTCP_async(string host, ushort port, Action<TcpClient, BinaryWriter, BinaryReader> onCommunication, bool logs = true, Action onError = null) => Task.Run(() => TryOpenSocketTCP_sync(host, port, onCommunication, logs, onError));
    public static void TryOpenSocketTCP_sync(in string host, in ushort port, in Action<TcpClient, BinaryWriter, BinaryReader> onCommunication, bool log = true, in Action onError = null)
    {
        string endPoint = $"{{ {host}:{port} }}";

        if (log)
            Debug.Log($"TCP connecting to: {endPoint}".ToSubLog());

        try
        {
            double t1 = Util.TotalMilliseconds;
            using TcpClient socket = new(host, port);

            if (socket.Connected)
            {
                using NetworkStream stream = socket.GetStream();
                using BinaryWriter writer = new(stream, Encoding.UTF8);
                using BinaryReader reader = new(stream, Encoding.UTF8);

                double t2 = Util.TotalMilliseconds - t1;
                if (log)
                    Debug.Log($"TCP connected to: {endPoint} ({t2.MillisecondsLog()})".ToSubLog());

                t1 = Util.TotalMilliseconds;
                onCommunication(socket, writer, reader);

                t2 = Util.TotalMilliseconds - t1;
                if (log)
                    Debug.Log($"TCP closing: {endPoint} ({t2.MillisecondsLog()})".ToSubLog());

                return;
            }
            else
                Debug.LogWarning($"TCP connection failure {endPoint}");
        }
        catch (SocketException e)
        {
            Debug.LogWarning($"{nameof(SocketException)} {endPoint} \"{e.Message.TrimEnd('\n', '\r', '\t', '\0')}\"");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        onError?.Invoke();
    }
}