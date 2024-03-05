using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpShell;
using SharpShell.Attributes;
using SharpShell.SharpNamespaceExtension;

namespace FriedGameFolder
{
    [ComVisible(true)]
    [DisplayName("FriedGameFolderNamespace")]
    [NamespaceExtensionJunctionPoint(NamespaceExtensionAvailability.Everyone, VirtualFolder.ControlPanel, "FriedGame")]
    public class Extention : SharpNamespaceExtension
    {
        public override NamespaceExtensionRegistrationSettings GetRegistrationSettings()
        {
            var settings = new NamespaceExtensionRegistrationSettings();
            settings.ExtensionAttributes 
                = AttributeFlags.CanByCopied
                | AttributeFlags.CanBeMoved
                | AttributeFlags.CanBeLinked
                | AttributeFlags.IsStorage
                | AttributeFlags.CanBeRenamed
                | AttributeFlags.CanBeDeleted
                | AttributeFlags.HasPropertySheets
                | AttributeFlags.IsDropTarget
                | AttributeFlags.IsFileSystemAncestor
                | AttributeFlags.IsFolder
                | AttributeFlags.IsFileSystem
                | AttributeFlags.MayContainSubFolders 
                | AttributeFlags.IsBrowsable
                | AttributeFlags.IsStorageAncestor
                ;
            settings.Tooltip = "FriedGames folder for playing games.";
            settings.UseAssemblyIcon = true;
            return settings;
        }

        protected override IEnumerable<IShellNamespaceItem> GetChildren(ShellNamespaceEnumerationFlags flags)
        {
            throw new NotImplementedException();
        }
        protected override ShellNamespaceFolderView GetView()
        {
            //this shit doest work
            return new CustomNamespaceFolderView(new GameForm());
        }

    }
}
