using UnityEngine;

partial class Util
{
    public static void ExecuteCommand(in string workingDir, in string command)
    {
        GUIUtility.systemCopyBuffer = command;

        const Colors cmd_color = Colors.pink;

        Debug.Log($"{typeof(Util).FullName}.{nameof(ExecuteCommand).Italic()}:\n\t{nameof(workingDir)}: {workingDir},\n\t{nameof(command)}: {command},".SetColor(cmd_color));

        System.Diagnostics.Process process = new()
        {
            StartInfo = new()
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDir
            }
        };

        try
        {
            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Debug.Log($"{"[OUTPUT]".SetColor(Colors.white)} {e.Data}".SetColor(cmd_color));
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Debug.LogWarning($"{"[ERROR]".SetColor(Colors.red)} {e.Data}".SetColor(cmd_color));
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            Debug.Log($"{"[EXIT_CODE]".SetColor(Colors.white)} {process.ExitCode}".SetColor(cmd_color));
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            process.Close();
        }
    }
}