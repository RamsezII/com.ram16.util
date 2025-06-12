using System;
using System.Runtime.InteropServices;
using UnityEngine;

partial class Util
{
    public static readonly bool is_windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    public static readonly bool is_linux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    public static readonly bool is_mac = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    public static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    //--------------------------------------------------------------------------------------------------------------

    public static void RunExternalCommand(in string work_dir, in string command_line, Action<string> on_stdout = null, Action<string> on_err = null)
    {
        static void LogStdout(string stdout) => Debug.Log(stdout);
        static void LogErr(string error) => Debug.LogWarning(error);

        on_stdout ??= LogStdout;
        on_err ??= LogErr;

        Debug.Log($"[CMD_start] {work_dir}$ {command_line}");

        using var process = new System.Diagnostics.Process();
        process.StartInfo.FileName = IsWindows() ? "powershell" : "/bin/bash";
        process.StartInfo.Arguments = IsWindows() ? $"/C {command_line}" : $"-c \"{command_line}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WorkingDirectory = work_dir;

        process.Start();

        string stdout = process.StandardOutput.ReadToEnd();
        string err = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (!string.IsNullOrWhiteSpace(stdout))
        {
            TrimNewline(ref stdout);
            on_stdout(stdout);
        }

        if (!string.IsNullOrWhiteSpace(err))
        {
            TrimNewline(ref err);
            on_err(err);
        }

        Debug.Log("[CMD_end]\n");
    }
}