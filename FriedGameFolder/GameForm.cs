using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using SharpShell.Interop;
using System.Runtime.ConstrainedExecution;
using System.Collections.Specialized;
using System.Drawing;
using System.Web;
using System.Text;

namespace FriedGameFolder
{


    public partial class GameForm : UserControl
    {
        public static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FriedGamesFolder");
        public static string CurrentPath = null;
        public static string CurrentRaw = null;
        public static string metadata = null;
        public static string desktopini = null;
        public List<FriedItem> Items = new List<FriedItem>();
        public static IntPtr CurrentHandle = IntPtr.Zero;

        public static bool loaded = false;

        public GameForm()
        {
            InitializeComponent();
            tmrLoad.Start();
        }

        private void tmrLoad_Tick(object sender, EventArgs e)
        {
            if (loaded)
            {
                flowFolder.Controls.Clear();
                foreach (var item in Items)
                {
                    if (item.Icon == null)
                        flowFolder.Controls.Add(new ItemFile(item.Path));
                    else
                        flowFolder.Controls.Add(new ItemFile(item.Path,item.Icon));
                }
                tmrLoad.Stop();
            }
            else
            { 
                tmrLoad.Stop();
                LoadMainExplorerWindowDetails();
                LoadBackground();
                lblPath.Text = " File:" + CurrentPath;
                //lblRaw.Text = " Raw:" + CurrentRaw;

                var directories = Directory.EnumerateDirectories(CurrentPath);
                foreach (var dir in directories)
                {
                    if (Path.GetFileName(dir).ToLower() == "$metadata") continue; //if its the metadata folder we skip
                    Items.Add(new FriedItem(dir,true));
                }

                var files = Directory.EnumerateFiles(CurrentPath);
                foreach (var file in files)
                {
                    if (Path.GetFileName(file).ToLower() == "desktop.ini") continue; //if its the desktop.ini we skip

                    Items.Add(new FriedItem(file, false,GetFileIcon(file)));
                }
                loaded = true;
                tmrLoad.Interval = 1000;
                tmrLoad.Start();
            }
        }

        private void LoadMainExplorerWindowDetails()
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
                        CurrentRaw = ie.LocationURL;
                        var decoded = CurrentRaw.Replace("file:///","");
                        decoded = HttpUtility.UrlDecode(decoded);
                        CurrentPath = decoded;
                        CurrentHandle = explorerWindowHandle;
                        if (!Directory.Exists(CurrentPath))
                        {
                            CurrentPath = Path.Combine(path, "Fallback");
                            Directory.CreateDirectory(CurrentPath);
                        }
                        metadata = Path.Combine(CurrentPath,"$metadata");
                        if (!Directory.Exists(metadata))
                        {
                            Directory.CreateDirectory(metadata);
                            DirectoryInfo metadataInfo = new DirectoryInfo(metadata);
                            metadataInfo.Attributes |= FileAttributes.Hidden | FileAttributes.System;
                        }
                        desktopini = Path.Combine(CurrentPath,"desktop.ini");
                        break; // Exit the loop since we found the correct File Explorer window
                    }

                }
            }
            lblPath.Text = " File:" + CurrentPath;

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
        private void GameForm_Load(object sender, EventArgs e)
        {
            tmrLoad.Start();
        }


        delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true)]
        static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }

        private Image GetFileIcon(string filePath)
        {
            Icon icon = Icon.ExtractAssociatedIcon(filePath);
            return icon?.ToBitmap();
        }

        private void flowFolder_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileData = e.Data.GetData(DataFormats.FileDrop);
                if (fileData is string[] fdat)
                {
                    StringCollection files = new StringCollection();
                    foreach (var f in fdat)
                    {
                        files.Add(f);
                    }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            var path2 = Path.Combine(CurrentPath, Path.GetFileName(file));
                            File.Copy(file, path2);
                        }
                        return;
                    }
                }
            }
        }

        private void flowFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                this.BackColor = Color.DarkGray;
            }
            else
            {
                e.Effect = DragDropEffects.None;
                this.flowFolder.BackColor = Color.White;
            }
        }

        private void flowFolder_DragLeave(object sender, EventArgs e)
        {
            this.flowFolder.BackColor = Color.White;
        }

        private void changeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG Files|*.png";
            openFileDialog.Title = "Select a PNG File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                string destinationPath = Path.Combine(metadata, "background.png");

                try
                {
                    File.Copy(selectedFilePath, destinationPath, true);
                    LoadBackground();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadBackground()
        {
            string backgroundImagePath = Path.Combine(metadata, "background.png");
            if (File.Exists(backgroundImagePath))
            {
                using (Image backgroundImage = Image.FromFile(backgroundImagePath))
                {
                    flowFolder.BackgroundImage = new Bitmap(backgroundImage);
                }
            }
            else
            {
                //MessageBox.Show("Background image file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void makeTransparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flowFolder.BackgroundImage = null;
            flowFolder.BackColor = Color.Fuchsia;
            TransparencyHelper.SetWindowTransparency(CurrentHandle,Color.Fuchsia,0);
        }
    }
    public class TransparencyHelper
    {
        // Constants
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x00080000;
        private const int LWA_COLORKEY = 0x00000001;

        // External functions
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        // Methods
        public static void SetWindowTransparency(IntPtr hWnd, Color colorKey, byte opacity)
        {
            int extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            extendedStyle |= WS_EX_LAYERED;
            SetWindowLong(hWnd, GWL_EXSTYLE, extendedStyle);

            SetLayeredWindowAttributes(hWnd, (uint)ColorTranslator.ToWin32(colorKey), opacity, LWA_COLORKEY);
        }
    }
}