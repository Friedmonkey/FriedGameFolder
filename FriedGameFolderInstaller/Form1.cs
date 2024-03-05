using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriedGameFolderInstaller
{
    public partial class Form1 : Form
    {
        public static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FriedGamesFolder");
        public Form1()
        {
            InitializeComponent();
        }

        private void bttnInstall_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);

            var extentionDll = Path.Combine(path, "FriedGameFolder.dll");
            File.Copy("FriedGameFolder.dll", extentionDll);

            var contextExtentionDll = Path.Combine(path, "FriedGameFolderContext.dll");
            File.Copy("FriedGameFolderContext.dll", contextExtentionDll);

            File.Copy("SharpShell.dll", Path.Combine(path, "SharpShell.dll"));


            if (ShellHelper.Install(extentionDll))
            {
                MessageBox.Show("Shell namespace extention install was a success, now installing context menu extention!");
                if (ShellHelper.Install(contextExtentionDll))
                {
                    MessageBox.Show("Shell context extention install was a success, retreving the CLSID!");

                    using (RegistryKey key = Registry.ClassesRoot.OpenSubKey($"{nameof(FriedGameFolder)}.{nameof(FriedGameFolder.Extention)}"))
                    {
                        if (key != null)
                        {
                            var CLSIDKEY = key.OpenSubKey("CLSID");
                            // Read the Default value, which contains the CLSID
                            string CLSID = CLSIDKEY.GetValue("") as string;

                            if (CLSID == null)
                            { 
                                MessageBox.Show("Error occured, failed to retreve the CLSID!");
                            }
                            else
                            { 
                                var text = $"[.ShellClassInfo]\r\nCLSID={CLSID}\r\n";
                                File.WriteAllText(Path.Combine(path,"desktop.txt"), text);

                                MessageBox.Show("Retriving the CLSID was a success, now restarting the file explorer to apply changes!");
                                ShellHelper.RestartExplorer();
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Error occured, failed installing context menu extention!");
                }
            }
            else
            {
                MessageBox.Show("Error occured, install failed!");
            }

        }

        private void bttnUninstall_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(path))
            {
                var extentionDll = Path.Combine(path, "FriedGameFolder.dll");
                var contextExtentionDll = Path.Combine(path, "FriedGameFolderContext.dll");

                if (ShellHelper.Uninstall(extentionDll))
                {
                    MessageBox.Show("Shell namespace extention Uninstall was a success, now Uninstalling context menu extention!");
                    if (ShellHelper.Uninstall(contextExtentionDll))
                    {
                        MessageBox.Show("Shell context extention Uninstall was a success, now restarting the file explorer to apply changes!");
                        ShellHelper.RestartExplorer();
                    }
                    else
                    {
                        MessageBox.Show("Error occured, Uninstall failed!");
                    }
                }
                else
                {
                    MessageBox.Show("Error occured, Uninstall failed!");
                }

                try
                {
                    Directory.Delete(path, true);
                }
                catch
                {
                    Thread.Sleep(500);
                    Directory.Delete(path, true);
                }
                finally 
                {
                    MessageBox.Show($"Couldnt delete \"{path}\"");
                }
            }
        }
    }
}
