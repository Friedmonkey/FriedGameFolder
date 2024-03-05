using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace FriedGameFolder
{


    public partial class GameForm : UserControl
    {
        public static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FriedGamesFolder");

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            tmrLoad.Start();
        }

        private void GetExplorerPath()
        {
            SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindows();
            foreach (SHDocVw.InternetExplorer ie in shellWindows)
            {
                string filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
                if (filename.Equals("explorer"))
                {
                    IntPtr explorerWindowHandle = (IntPtr)ie.HWND; // Get handle of the File Explorer window

                    var handles = GetChildWindowHandles(explorerWindowHandle);

                    if (handles.Contains(this.Handle))
                    {

                        Console.WriteLine("Explorer location : {0}", ie.LocationURL);
                        lblPath.Text += ie.LocationURL;
                        break; // Exit the loop since we found the correct File Explorer window
                    }

                }
            }
        }

        private List<IntPtr> GetChildWindowHandles(IntPtr mainWindowHandle)
        {
            List<IntPtr> childWindowHandles = new List<IntPtr>();
            EnumChildWindows(mainWindowHandle, (hwnd, lParam) =>
            {
                childWindowHandles.Add(hwnd);
                return true; // Continue enumeration
            }, IntPtr.Zero);

            return childWindowHandles;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);

        delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

        private void tmrLoad_Tick(object sender, EventArgs e)
        {
            tmrLoad.Stop();
            //MessageBox.Show("Getting path!");
            GetExplorerPath();
        }
    }
}