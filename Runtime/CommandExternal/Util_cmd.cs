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

    public static void RunExternalCommand(in string work_dir, in string command, in Action<string> on_stdout = null, in Action<string> on_err = null)
    {
        using var process = new System.Diagnostics.Process();
        process.StartInfo.FileName = IsWindows() ? "powershell" : "/bin/bash";
        process.StartInfo.Arguments = IsWindows() ? $"/C {command}" : $"-c \"{command}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WorkingDirectory = work_dir;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (output.EndsWith('\n'))
            output = output[..^1];

        if (error.EndsWith('\n'))
            error = error[..^1];

        if (on_stdout == null)
            Debug.Log(output);
        else
            on_stdout(output);

        if (!string.IsNullOrWhiteSpace(error))
            if (on_err == null)
                Debug.LogWarning(error);
            else
                on_err(error);
    }
}