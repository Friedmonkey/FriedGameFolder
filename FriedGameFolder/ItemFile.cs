using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriedGameFolder
{
    public partial class ItemFile : UserControl
    {
        private Color LineColor = Color.FromArgb(0x80, 0xAA, 0xD5, 0xE3);
        private bool hoverd = false;
        private string filename = "";
        public string FileName
        {
            get
            {
                return filename;
            }
            set
            {
                lblFilename.Text = Path.GetFileName(value);
                filename = value;
            }
        }

        public Image Icon
        {
            get => picbxIcon.Image;
            set => picbxIcon.Image = value;
        }
        public ItemFile() => Base();
        public ItemFile(string path) 
        {
            Base();
            this.FileName = path;
        }
        public ItemFile(string path, Image icon)
        {
            Base();
            this.FileName = path;
            this.Icon = icon;
        }
        public void Base() 
        {
            InitializeComponent();
            base.CreateParams.ExStyle |= 0x20;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF);
        }

        private void ItemFile_MouseDown(object sender, MouseEventArgs e)
        {
            StringCollection filePath = new StringCollection() { filename };
            DataObject dataObject = new DataObject();

            dataObject.SetFileDropList(filePath);

            this.DoDragDrop(dataObject, DragDropEffects.Move);
        }

        private void ItemFile_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(0x80, 0xAD, 0xD8, 0xE6);
            hoverd = true;
        }

        private void ItemFile_MouseLeave(object sender, EventArgs e)
        {
            if (this.GetChildAtPoint(this.PointToClient(MousePosition)) == null)
            {
                BackColor = Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF);
                hoverd = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (hoverd)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, LineColor, ButtonBorderStyle.Solid);
            }
        }
    }
}
