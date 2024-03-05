using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedGameFolderInstaller
{
    public static class ShellHelper
    {
        public static void RestartExplorer()
        {
            Process explorerProcess = Process.GetProcesses().FirstOrDefault((Process p) => p.ProcessName == "explorer");
            if (explorerProcess != null)
            {
                explorerProcess.Kill();
            }
        }
        public static bool Install(string assemblyPath)
        {
            string args = "/codebase" + " \"" + assemblyPath + "\"";
            return Execute(GetRegAsmPath(), args);
        }
        public static bool Uninstall(string assemblyPath)
        {
            string args = "/u \"" + assemblyPath + "\"";
            return Execute(GetRegAsmPath(), args);
        }
        public static string GetRegAsmPath()
        {
            string frameworkFolder = "Framework64";
            string searchRoot = Path.Combine(Environment.ExpandEnvironmentVariables("%WINDIR%"), "Microsoft.Net", frameworkFolder);
            string[] candidates = (from c in Directory.GetDirectories(searchRoot, "v*", SearchOption.TopDirectoryOnly)
                                   select Path.Combine(c, "regasm.exe")).ToArray<string>();
            string path = (from s in candidates
                           orderby s descending
                           select s).Where(new Func<string, bool>(File.Exists)).FirstOrDefault<string>();
            if (path == null)
            {
                throw new InvalidOperationException(string.Concat(new string[]
                {
                    "Failed to find regasm in '",
                    searchRoot,
                    "'. Checked: ",
                    Environment.NewLine,
                    string.Join(Environment.NewLine, candidates)
                }));
            }
            return path;
        }
        private static bool Execute(string regasmPath, string arguments)
        {
            Process regasm = new Process
            {
                StartInfo =
                {
                    FileName = regasmPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("RegAsm: preparing to run " + regasmPath + " " + arguments);
            Console.ResetColor();
            regasm.Start();
            regasm.WaitForExit();
            var StandardOutput = regasm.StandardOutput.ReadToEnd();
            var StandardError = regasm.StandardError.ReadToEnd();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("RegAsm: exited with code {0}", regasm.ExitCode));
            Console.ResetColor();
            if (!string.IsNullOrEmpty(StandardOutput))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("RegAsm: Output: " + StandardOutput);
                Console.ResetColor();
            }
            if (!string.IsNullOrEmpty(StandardError))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("RegAsm: Error Output: " + StandardError);
                Console.ResetColor();
            }
            return regasm.ExitCode == 0;
        }
    }
}
