using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedGameFolder
{
    public class FriedItem
    {
        public FriedItem() { } 

        public FriedItem(string path, bool isfolder, Image icon = null, Point? pos = null) 
        {
            this.Path = path;
            this.IsFolder = isfolder;
            this.Icon = icon;
            this.Position = pos;
        }
        public string Path { get; set; }
        public bool IsFolder { get; set; }
        public Point? Position { get; set; }
        public Image Icon { get; set; } = null;
    }
}
