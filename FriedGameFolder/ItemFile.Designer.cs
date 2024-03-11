namespace FriedGameFolder
{
    partial class ItemFile
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemFile));
            this.picbxIcon = new System.Windows.Forms.PictureBox();
            this.lblFilename = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picbxIcon
            // 
            this.picbxIcon.Image = ((System.Drawing.Image)(resources.GetObject("picbxIcon.Image")));
            this.picbxIcon.Location = new System.Drawing.Point(15, 5);
            this.picbxIcon.Name = "picbxIcon";
            this.picbxIcon.Size = new System.Drawing.Size(120, 105);
            this.picbxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbxIcon.TabIndex = 0;
            this.picbxIcon.TabStop = false;
            this.picbxIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemFile_MouseDown);
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(15, 115);
            this.lblFilename.MaximumSize = new System.Drawing.Size(120, 0);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(63, 13);
            this.lblFilename.TabIndex = 2;
            this.lblFilename.Text = "ExampleFile";
            this.lblFilename.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemFile_MouseDown);
            // 
            // ItemFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.picbxIcon);
            this.Name = "ItemFile";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemFile_MouseDown);
            this.MouseEnter += new System.EventHandler(this.ItemFile_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ItemFile_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.picbxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picbxIcon;
        private System.Windows.Forms.Label lblFilename;
    }
}
