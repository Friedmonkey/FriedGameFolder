using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FriedExplorerExtractor
{
    internal class Program
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        static void Main(string[] args)
        {
            if (args.Length == 0) 
            {
                args = new string[] { "9936" };
            }
            File.AppendAllText(@"C:\Users\marti\AppData\Roaming\FriedGamesFolder\debug.txt", "\nInput:" + args[0]);
            //IntPtr MyHwnd = FindWindow(null, "BallZ");
            IntPtr MyHwnd = new IntPtr(int.Parse(args[0]));

            MyHwnd = FindWindow(null, "Ball");

            var t = Type.GetTypeFromProgID("Shell.Application");
            dynamic o = Activator.CreateInstance(t);
            try
            {
                var ws = o.Windows();
                for (int i = 0; i < ws.Count; i++)
                {
                    var ie = ws.Item(i);
                    if (ie == null || ie.hwnd != (long)MyHwnd) continue;
                    var path = System.IO.Path.GetFileName((string)ie.FullName);
                    if (path.ToLower() == "explorer.exe")
                    {
                        var explorepath = ie.document.focuseditem.path;
                        //Console.WriteLine("FilePath:"+explorepath);
                        //File.AppendAllText(@"C:\Users\marti\AppData\Roaming\FriedGamesFolder\debug.txt", "\nFilePath:" + explorepath);
                    }
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(o);
            }
        }
    }
}
