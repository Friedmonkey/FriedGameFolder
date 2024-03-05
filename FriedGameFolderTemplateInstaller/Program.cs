using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriedGameFolderTemplateInstaller
{
    internal class Program
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetFileAttributesW(string lpFileName, uint dwFileAttributes);

        public const uint FILE_ATTRIBUTE_ARCHIVE = 32;
        public const uint FILE_ATTRIBUTE_HIDDEN = 2;
        public const uint FILE_ATTRIBUTE_SYSTEM = 4;



        public static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FriedGamesFolder");
        public static readonly string CLSID = "{F5175861-2688-11d0-9C5E-00AA00A45957}";
        static void Main(string[] args)
        {
            if (args.Length < 0)
                return;

            MessageBox.Show($"Creating folder in \"{args[0]}\"");

            //create the folder
            var folder = Path.Combine(args[0], "New Games Folder");
            Directory.CreateDirectory(folder);

            //create the desktop.ini file
            var text = $"[.ShellClassInfo]\r\nCLSID={CLSID}\r\n";
            var desktopini = Path.Combine(folder, "desktop.ini");

            //make the file
            File.WriteAllText(desktopini,text);

            //set the correct file attributes
            uint fileAttributes = FILE_ATTRIBUTE_ARCHIVE | FILE_ATTRIBUTE_HIDDEN | FILE_ATTRIBUTE_SYSTEM;
            SetFileAttributesW(desktopini, fileAttributes);

        }
    }
}
