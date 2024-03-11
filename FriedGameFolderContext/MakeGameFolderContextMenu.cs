using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpShell;
using SharpShell.Attributes;
using SharpShell.Interop;
using SharpShell.SharpContextMenu;
using SharpShell.SharpNamespaceExtension;

namespace FriedGameFolderContext
{
    [ComVisible(true)]
    [DisplayName("FriedGameFolderContext")]
    [COMServerAssociation(AssociationType.DirectoryBackground)]
    public class MakeGameFolderContextMenu : SharpContextMenu
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetFileAttributesW(string lpFileName, uint dwFileAttributes);

        public const uint FILE_ATTRIBUTE_ARCHIVE = 32;
        public const uint FILE_ATTRIBUTE_HIDDEN = 2;
        public const uint FILE_ATTRIBUTE_SYSTEM = 4;



        public static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FriedGamesFolder");
        //public static readonly string CLSID = "{a8a104e0-953f-3de8-b94a-4ba75b0d39b3}";
        public static readonly string DesktopIniContent = File.ReadAllText(Path.Combine(path, "desktop.txt"));

        protected override bool CanShowMenu()
        {
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            var menu = new ContextMenuStrip();

            var create = new ToolStripMenuItem()
            {
                Text = "Create new games folder",
            };
            create.Click += (sender, args) => { CreateFolder(sender, args); };

            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(create);
            menu.Items.Add(new ToolStripSeparator());
            return menu;
            //throw new NotImplementedException();
        }

        private void CreateFolder(object sender, EventArgs args)
        {
            string directory = FolderPath;
            //MessageBox.Show($"Creating folder in \"{directory}\"");

            //create the folder
            var folder = Path.Combine(directory, "New Games Folder");
            Directory.CreateDirectory(folder);

            //create the desktop.ini file
            //var text = $"[.ShellClassInfo]\r\nCLSID={CLSID}\r\n";
            var desktopini = Path.Combine(folder, "desktop.ini");

            //make the file
            File.WriteAllText(desktopini, DesktopIniContent);

            //set the correct file attributes
            uint fileAttributes = FILE_ATTRIBUTE_ARCHIVE | FILE_ATTRIBUTE_HIDDEN | FILE_ATTRIBUTE_SYSTEM;
            SetFileAttributesW(desktopini, fileAttributes);

            DirectoryInfo folderInfo = new DirectoryInfo(folder);
            folderInfo.Attributes |= FileAttributes.System;


            //metadata folder
            var metadata = Path.Combine(folder, "$metadata");

            Directory.CreateDirectory(metadata);

            DirectoryInfo metadataInfo = new DirectoryInfo(metadata);
            metadataInfo.Attributes |= FileAttributes.Hidden | FileAttributes.System;

        }
    }

    [ComVisible(true)]
    [DisplayName("FriedGameFolderToggleContext")]
    [COMServerAssociation(AssociationType.Folder)]
    public class ToggleGameFolderContextMenu : SharpContextMenu
    {

        public static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FriedGamesFolder");
        //public static readonly string CLSID = "{a8a104e0-953f-3de8-b94a-4ba75b0d39b3}";
        public static readonly string DesktopIniContent = File.ReadAllText(Path.Combine(path,"desktop.txt"));

        protected override bool CanShowMenu()
        {
            if (SelectedItemPaths.Count() > 0)
            { 
                var tPath = SelectedItemPaths.First();
                var tFile = Path.Combine(tPath, "desktop.ini");
                if (File.Exists(tFile))
                { 
                    var content = File.ReadAllText(tFile).Trim();
                    if (content == DesktopIniContent.Trim())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            var menu = new ContextMenuStrip();
            if (SelectedItemPaths.Count() > 0)
            {
                var tPath = SelectedItemPaths.First();
                DirectoryInfo info = new DirectoryInfo(tPath);
                if (info.Attributes.HasFlag(FileAttributes.System))
                {
                    var Disable = new ToolStripMenuItem()
                    {
                        Text = "Disable Games Folder",
                    };
                    Disable.Click += (sender, args) =>
                    {
                        // Clear the System attribute to disable the folder
                        info.Attributes &= ~FileAttributes.System;
                    };

                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add(Disable);
                    menu.Items.Add(new ToolStripSeparator());
                }
                else
                {
                    var Enable = new ToolStripMenuItem()
                    {
                        Text = "Enable Games Folder",
                    };
                    Enable.Click += (sender, args) =>
                    {
                        // Set the System attribute to enable the folder
                        info.Attributes |= FileAttributes.System;
                    };

                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add(Enable);
                    menu.Items.Add(new ToolStripSeparator());
                }
            }
            return menu;
        }

    }
}
