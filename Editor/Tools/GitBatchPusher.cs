using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _UTIL_e
{
    public static class GitBatchPusher
    {
        public static IEnumerator<float> EPushAllGitRepos(string commitMessage)
        {
            string[] dirs = Directory.GetDirectories(Application.dataPath);
            int pushed = 0;
            float countf = dirs.Length;

            for (int i = 0; i < dirs.Length; i++)
            {
                yield return i / countf;

                string dir = dirs[i];
                string gitDir = Path.Combine(dir, ".git");
                if (!Directory.Exists(gitDir))
                    continue;

                if (RunGitCommands(dir, commitMessage))
                    ++pushed;

                Debug.Log("\n\n--------------------------------------------------------------------------------------------------------------\n\n");
            }

            RunGitCommands(Directory.GetParent(Application.dataPath).FullName, commitMessage);

            Debug.Log($"\n\n{typeof(GitBatchPusher)} pushed {pushed} repo(s)\n");
        }

        public static void PushAllGitRepos(in string commitMessage)
        {
            var routine = EPushAllGitRepos(commitMessage);
            while (routine.MoveNext()) ;
        }

        static bool RunGitCommands(string path, string message)
        {
            try
            {
                Debug.Log($"\n-----  {Path.GetFileName(path)}  -----\n");
                RunGit("add .", path);
                RunGit($"commit -m \"{message}\"", path);
                RunGit("push", path);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Git failed for {path} : {e.Message}");
                return false;
            }
        }

        static void RunGit(string args, string workingDir)
        {
            System.Diagnostics.ProcessStartInfo psi = new("git", args)
            {
                WorkingDirectory = workingDir,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var p = System.Diagnostics.Process.Start(psi);
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();

            if (!string.IsNullOrWhiteSpace(output)) Debug.Log(output);
            if (!string.IsNullOrWhiteSpace(error)) Debug.LogWarning(error);

            if (p.ExitCode != 0)
                throw new Exception($"Git command failed : {args}");
        }
    }
}